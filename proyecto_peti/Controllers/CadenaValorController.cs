using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using proyecto_peti.Models;

namespace proyecto_peti.Controllers
{
    public class CadenaValorController : Controller
    {
        private Modelo db = new Modelo();

        public ActionResult Index()
        {
            if (Session["PlanId"] == null)
                return RedirectToAction("Login", "Account");

            int planId = (int)Session["PlanId"];
            var preguntas = ObtenerPreguntasCadenaValor();

            var respuestasExistentes = db.CadenaValor
                .Where(c => c.PlanId == planId)
                .OrderBy(c => c.PreguntaNumero)
                .ToList();

            // Crear registros de preguntas si no existen
            if (!respuestasExistentes.Any())
            {
                foreach (var pregunta in preguntas)
                {
                    db.CadenaValor.Add(new CadenaValor
                    {
                        PlanId = planId,
                        PreguntaNumero = pregunta.Key,
                        PreguntaTexto = pregunta.Value,
                        Valoracion = 0, // valor inicial neutro
                        CreatedAt = DateTime.UtcNow
                    });
                }
                db.SaveChanges();

                respuestasExistentes = db.CadenaValor
                    .Where(c => c.PlanId == planId)
                    .OrderBy(c => c.PreguntaNumero)
                    .ToList();
            }

            var fortalezasDebilidades = db.ObservacionesCadenaValor.FirstOrDefault(f => f.PlanId == planId)
                                           ?? new ObservacionesCadenaValor { PlanId = planId };

            // Calcular Potencial de Mejora
            double? potencialMejora = null;
            var valoraciones = respuestasExistentes.Where(r => r.Valoracion >= 1 && r.Valoracion <= 5).ToList();
            if (valoraciones.Any())
            {
                var promedio = valoraciones.Average(r => r.Valoracion);
                // Por ejemplo: potencial de mejora = 5 - promedio (más bajo es mejor, más alto es más mejora)
                potencialMejora = 5 - promedio;
            }

            ViewBag.FortalezasDebilidades = fortalezasDebilidades;
            ViewBag.PotencialMejora = potencialMejora;

            return View(respuestasExistentes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(List<CadenaValor> modelo, ObservacionesCadenaValor fortalezasDebilidades)
        {
            if (Session["PlanId"] == null)
                return RedirectToAction("Login", "Account");

            int planId = (int)Session["PlanId"];

            if (modelo == null || modelo.Any(m => m.Valoracion < 1 || m.Valoracion > 5))
            {
                ModelState.AddModelError("", "Todas las valoraciones deben estar entre 1 y 5.");
                ViewBag.FortalezasDebilidades = fortalezasDebilidades;
                // También recalcular potencial para mostrar al volver a vista
                var potencialMejora = modelo?.Where(m => m.Valoracion >= 1 && m.Valoracion <= 5).Any() == true
                    ? 5 - modelo.Where(m => m.Valoracion >= 1 && m.Valoracion <= 5).Average(m => m.Valoracion)
                    : (double?)null;
                ViewBag.PotencialMejora = potencialMejora;
                return View(modelo);
            }

            var fortalezas = new[] { fortalezasDebilidades.Fortalezas1, fortalezasDebilidades.Fortalezas2, fortalezasDebilidades.Fortalezas3, fortalezasDebilidades.Fortalezas4 };
            var debilidades = new[] { fortalezasDebilidades.Debilidades1, fortalezasDebilidades.Debilidades2, fortalezasDebilidades.Debilidades3, fortalezasDebilidades.Debilidades4 };

            if (fortalezas.All(string.IsNullOrWhiteSpace) || debilidades.All(string.IsNullOrWhiteSpace))
            {
                ModelState.AddModelError("", "Debe ingresar al menos una fortaleza y una debilidad.");
                ViewBag.FortalezasDebilidades = fortalezasDebilidades;
                var potencialMejora = modelo?.Where(m => m.Valoracion >= 1 && m.Valoracion <= 5).Any() == true
                    ? 5 - modelo.Where(m => m.Valoracion >= 1 && m.Valoracion <= 5).Average(m => m.Valoracion)
                    : (double?)null;
                ViewBag.PotencialMejora = potencialMejora;
                return View(modelo);
            }

            foreach (var item in modelo)
            {
                var existente = db.CadenaValor.FirstOrDefault(c => c.PlanId == planId && c.PreguntaNumero == item.PreguntaNumero);
                if (existente != null)
                {
                    existente.Valoracion = item.Valoracion;
                    existente.UpdatedAt = DateTime.UtcNow;
                }
            }

            var fdExistente = db.ObservacionesCadenaValor.FirstOrDefault(f => f.PlanId == planId);
            if (fdExistente != null)
            {
                fdExistente.Fortalezas1 = fortalezasDebilidades.Fortalezas1;
                fdExistente.Fortalezas2 = fortalezasDebilidades.Fortalezas2;
                fdExistente.Fortalezas3 = fortalezasDebilidades.Fortalezas3;
                fdExistente.Fortalezas4 = fortalezasDebilidades.Fortalezas4;

                fdExistente.Debilidades1 = fortalezasDebilidades.Debilidades1;
                fdExistente.Debilidades2 = fortalezasDebilidades.Debilidades2;
                fdExistente.Debilidades3 = fortalezasDebilidades.Debilidades3;
                fdExistente.Debilidades4 = fortalezasDebilidades.Debilidades4;

                fdExistente.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                fortalezasDebilidades.PlanId = planId;
                fortalezasDebilidades.CreatedAt = DateTime.UtcNow;
                db.ObservacionesCadenaValor.Add(fortalezasDebilidades);
            }

            db.SaveChanges();

            TempData["SuccessMessage"] = "Datos guardados correctamente.";
            return RedirectToAction("Index", "ObjetivosEstrategicos");
        }

        private Dictionary<int, string> ObtenerPreguntasCadenaValor()
        {
            return new Dictionary<int, string>
            {
                {1, "La empresa tiene una política sistematizada de cero defectos en la producción de productos/servicios."},
                {2, "La empresa emplea los medios productivos tecnológicamente más avanzados de su sector."},
                {3, "La empresa dispone de un sistema de información y control de gestión eficiente y eficaz."},
                {4, "Los medios técnicos y técnológicos de la empresa están preparados para competir en un futuro a corto, medio y largo plazo."},
                {5, "La empresa es un referente en su sector en I+D+i."},
                {6, "La excelencia de los procedimientos de la empresa (en ISO, etc.) son una principal fuente de ventaja competiva."},
                {7, "La empresa dispone de página web, y esta se emplea no sólo como escaparate virtual de productos/servicios, sino también para establecer relaciones con clientes y proveedores."},
                {8, "Los productos/servicios que desarrolla nuestra empresa llevan incorporada una tecnología difícil de imitar."},
                {9, "La empresa es referente en su sector en la optimización, en términos de coste, de su cadena de producción, siendo ésta una de sus principales ventajas competitivas."},
                {10, "La informatización de la empresa es una fuente de ventaja competitiva clara respecto a sus competidores."},
                {11, "Los canales de distribución de la empresa son una importante fuente de ventajas competitivas."},
                {12, "Los productos/servicios de la empresa son altamente, y diferencialmente, valorados por el cliente respecto a nuestros competidores."},
                {13, "La empresa dispone y ejecuta un sistematico plan de marketing y ventas."},
                {14, "La empresa tiene optimizada su gestión financiera."},
                {15, "La empresa busca continuamente el mejorar la relación con sus clientes cortando los plazos de ejecución, personalizando la oferta o mejorando las condiciones de entrega. Pero siempre partiendo de un plan previo."},
                {16, "La empresa es referente en su sector en el lanzamiento de innovadores productos y servicio de éxito demostrado en el mercado."},
                {17, "Los Recursos Humanos son especialmente responsables del éxito de la empresa, considerándolos incluso como el principal activo estratégico."},
                {18, "Se tiene una plantilla altamente motivada, que conoce con claridad las metas, objetivos y estrategias de la organización."},
                {19, "La empresa siempre trabaja conforme a una estrategia y objetivos claros."},
                {20, "La gestión del circulante está optimizada."},
                {21, "Se tiene definido claramente el posicionamiento estratégico de todos los productos de la empresa."},
                {22, "Se dispone de una política de marca basada en la reputación que la empresa genera, en la gestión de relación con el cliente y en el posicionamiento estratégico previamente definido."},
                {23, "La cartera de clientes de nuestra empresa está altamente fidelizada, ya que tenemos como principal propósito el deleitarlos día a día."},
                {24, "Nuestra política y equipo de ventas y marketing es una importante ventaja competitiva de nuestra empresa respecto al sector."},
                {25, "El servicio al cliente que prestamos es uno de nuestras principales ventajas competitivas respecto a nuestros competidores."}
            };
        }
    }
}
