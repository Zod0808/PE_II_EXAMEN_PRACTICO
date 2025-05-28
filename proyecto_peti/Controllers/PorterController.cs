using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using proyecto_peti.Models;

namespace proyecto_peti.Controllers
{
    public class PorterController : Controller
    {
        private Modelo db = new Modelo();

        public ActionResult Index()
        {
            try
            {
                if (Session["PlanId"] == null)
                    return RedirectToAction("Login", "Account");

                int planId = (int)Session["PlanId"];
                var modelo = db.FuerzasPorter.FirstOrDefault(x => x.PlanId == planId);

                if (modelo == null)
                {
                    modelo = new FuerzasPorter
                    {
                        PlanId = planId,
                        // Inicializar campos con valores por defecto para evitar nulos
                        AmenazaNuevos = "",
                        RivalidadCompetidores = "",
                        PoderClientes = "",
                        PoderProveedores = "",
                        AmenazaSustitutos = ""
                    };
                }

                return View(modelo);
            }
            catch (EntityCommandExecutionException ex)
            {
                // Log del error específico
                var innerMessage = ex.InnerException?.Message ?? "Sin detalles adicionales";
                ViewBag.ErrorMessage = $"Error de base de datos: {ex.Message} - Detalles: {innerMessage}";

                // Retornar modelo vacío para evitar errores en la vista
                return View(new FuerzasPorter { PlanId = (int)(Session["PlanId"] ?? 0) });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error inesperado: {ex.Message}";
                return View(new FuerzasPorter { PlanId = (int)(Session["PlanId"] ?? 0) });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Agregar protección CSRF
        public ActionResult Index(FuerzasPorter model)
        {
            try
            {
                if (Session["PlanId"] == null)
                    return RedirectToAction("Login", "Account");

                int planId = (int)Session["PlanId"];

                // Validar que el modelo no esté vacío
                if (model == null)
                {
                    ModelState.AddModelError("", "Datos del formulario inválidos");
                    return View(new FuerzasPorter { PlanId = planId });
                }

                // Asegurar que el PlanId coincida
                model.PlanId = planId;

                // Validar longitud de campos para evitar errores de base de datos
                model.AmenazaNuevos = ValidarLongitudTexto(model.AmenazaNuevos, 1000);
                model.RivalidadCompetidores = ValidarLongitudTexto(model.RivalidadCompetidores, 1000);
                model.PoderClientes = ValidarLongitudTexto(model.PoderClientes, 1000);
                model.PoderProveedores = ValidarLongitudTexto(model.PoderProveedores, 1000);
                model.AmenazaSustitutos = ValidarLongitudTexto(model.AmenazaSustitutos, 1000);

                var existente = db.FuerzasPorter.FirstOrDefault(x => x.PlanId == planId);

                if (existente != null)
                {
                    // Actualizar registro existente
                    existente.AmenazaNuevos = model.AmenazaNuevos;
                    existente.RivalidadCompetidores = model.RivalidadCompetidores;
                    existente.PoderClientes = model.PoderClientes;
                    existente.PoderProveedores = model.PoderProveedores;
                    existente.AmenazaSustitutos = model.AmenazaSustitutos;

                    // Si tu modelo tiene campos de auditoría
                    if (HasProperty(existente, "UpdatedAt"))
                    {
                        SetProperty(existente, "UpdatedAt", DateTime.UtcNow);
                    }
                }
                else
                {
                    // Crear nuevo registro
                    var nuevoRegistro = new FuerzasPorter
                    {
                        PlanId = planId,
                        AmenazaNuevos = model.AmenazaNuevos,
                        RivalidadCompetidores = model.RivalidadCompetidores,
                        PoderClientes = model.PoderClientes,
                        PoderProveedores = model.PoderProveedores,
                        AmenazaSustitutos = model.AmenazaSustitutos
                    };

                    // Si tu modelo tiene campos de auditoría
                    if (HasProperty(nuevoRegistro, "CreatedAt"))
                    {
                        SetProperty(nuevoRegistro, "CreatedAt", DateTime.UtcNow);
                    }

                    db.FuerzasPorter.Add(nuevoRegistro);
                }

                db.SaveChanges();

                TempData["SuccessMessage"] = "Análisis de las 5 Fuerzas de Porter guardado correctamente.";
                return RedirectToAction("Index", "PEST");
            }
            catch (DbEntityValidationException ex)
            {
                // Errores de validación de Entity Framework
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => $"Campo: {x.PropertyName}, Error: {x.ErrorMessage}")
                    .ToList();

                ModelState.AddModelError("", "Errores de validación: " + string.Join("; ", errorMessages));
                return View(model);
            }
            catch (EntityCommandExecutionException ex)
            {
                // Error específico de ejecución de comando SQL
                var innerMessage = ex.InnerException?.Message ?? "Sin detalles adicionales";
                ModelState.AddModelError("", $"Error de base de datos: {innerMessage}");

                // Log detallado para debugging
                System.Diagnostics.Debug.WriteLine($"EntityCommandExecutionException: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Inner Exception: {innerMessage}");

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error inesperado al guardar: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Error en PorterController: {ex.Message}");
                return View(model);
            }
        }

        // Método auxiliar para validar longitud de texto
        private string ValidarLongitudTexto(string texto, int longitudMaxima)
        {
            if (string.IsNullOrEmpty(texto))
                return string.Empty; // Retornar cadena vacía en lugar de null

            return texto.Length > longitudMaxima ? texto.Substring(0, longitudMaxima) : texto;
        }

        // Métodos auxiliares para manejar propiedades dinámicamente
        private bool HasProperty(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }

        private void SetProperty(object obj, string propertyName, object value)
        {
            var property = obj.GetType().GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(obj, value);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}