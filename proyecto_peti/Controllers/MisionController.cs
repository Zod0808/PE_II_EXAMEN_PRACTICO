using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyecto_peti.Models;

namespace proyecto_peti.Controllers
{
    public class MisionController : Controller
    {
        private Modelo db = new Modelo();

        // GET: Mision
        public ActionResult Index()
        {
            // Validar sesión
            if (Session["PlanId"] == null)
                return RedirectToAction("Login", "Account");

            int planId = (int)Session["PlanId"];

            // Obtener o crear nueva misión para este plan
            var mision = db.Mision.FirstOrDefault(m => m.PlanId == planId);
            if (mision == null)
            {
                mision = new Mision { PlanId = planId };
            }

            return View(mision);
        }

        [HttpPost]
        public ActionResult Index(Mision model)
        {
            if (Session["PlanId"] == null)
                return RedirectToAction("Login", "Account");

            int planId = (int)Session["PlanId"];

            var existente = db.Mision.FirstOrDefault(m => m.PlanId == planId);
            if (existente != null)
            {
                existente.Contenido = model.Contenido;
                existente.UpdatedAt = System.DateTime.Now;
            }
            else
            {
                model.PlanId = planId;
                model.CreatedAt = System.DateTime.Now;
                db.Mision.Add(model);
            }

            db.SaveChanges();

            return RedirectToAction("Index", "Vision");
        }
    }
}