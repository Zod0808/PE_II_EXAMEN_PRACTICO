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

        // GET: CadenaValor
        public ActionResult Index()
        {
            if (Session["PlanId"] == null)
                return RedirectToAction("Login", "Account");

            int planId = (int)Session["PlanId"];

            // Obtener preguntas de la cadena de valor
            var preguntas = db.PreguntasCadenaValor
                             .Where(p => p.PlanId == planId)
                             .ToList();

            // Obtener o crear observaciones (fortalezas/debilidades)
            var fortalezasDebilidades = db.ObservacionesCadenaValor
                                        .FirstOrDefault(o => o.PlanId == planId)
                                        ?? new ObservacionesCadenaValor { PlanId = planId };

            ViewBag.FortalezasDebilidades = fortalezasDebilidades;
            ViewBag.PotencialMejora = CalcularPotencialMejora(preguntas); // (Método personalizado)

            return View(preguntas); // Pasar el modelo a la vista
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(List<PreguntasCadenaValor> model, ObservacionesCadenaValor observaciones)
        {
            if (Session["PlanId"] == null)
                return RedirectToAction("Login", "Account");

            int planId = (int)Session["PlanId"];

            if (ModelState.IsValid)
            {
                // Guardar preguntas
                foreach (var pregunta in model)
                {
                    pregunta.PlanId = planId;
                    if (pregunta.Id == 0)
                        db.PreguntasCadenaValor.Add(pregunta);
                    else
                        db.Entry(pregunta).State = System.Data.Entity.EntityState.Modified;
                }

                // Guardar observaciones
                observaciones.PlanId = planId;
                if (observaciones.Id == 0)
                    db.ObservacionesCadenaValor.Add(observaciones);
                else
                    db.Entry(observaciones).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
                TempData["SuccessMessage"] = "Datos guardados correctamente.";
                return RedirectToAction("Index");
            }

            // Si hay errores, recargar vista con datos
            ViewBag.FortalezasDebilidades = observaciones;
            return View(model);
        }

        private double? CalcularPotencialMejora(List<PreguntasCadenaValor> preguntas)
        {
            if (preguntas == null || !preguntas.Any())
                return null;

            return preguntas.Average(p => p.Valoracion) * 20; // Ejemplo: Escala de 1-5 a porcentaje
        }
    }
}