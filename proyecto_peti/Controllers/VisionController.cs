using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyecto_peti.Models;

namespace proyecto_peti.Controllers
{
    public class VisionController : Controller
    {
        private Modelo db = new Modelo();

        // GET: Vision
        public ActionResult Index()
        {
            if (Session["PlanId"] == null)
                return RedirectToAction("Login", "Account");

            int planId = (int)Session["PlanId"];

            var vision = db.Vision.FirstOrDefault(v => v.PlanId == planId);
            if (vision == null)
            {
                vision = new Vision { PlanId = planId };
            }

            ViewBag.VisionTexto = vision.Contenido;
            return View(vision);
        }

        [HttpPost]
        public ActionResult Index(Vision model)
        {
            if (Session["PlanId"] == null)
                return RedirectToAction("Login", "Account");

            int planId = (int)Session["PlanId"];

            var existente = db.Vision.FirstOrDefault(v => v.PlanId == planId);
            if (existente != null)
            {
                existente.Contenido = model.Contenido;
                existente.UpdatedAt = DateTime.Now;
            }
            else
            {
                model.PlanId = planId;
                model.CreatedAt = DateTime.Now;
                db.Vision.Add(model);
            }

            db.SaveChanges();

            return RedirectToAction("Index", "Valores");
        }
    }
}