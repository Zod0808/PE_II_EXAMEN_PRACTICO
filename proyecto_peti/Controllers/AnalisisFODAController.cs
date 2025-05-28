using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyecto_peti.Models;

namespace proyecto_peti.Controllers
{
    public class AnalisisFODAController : Controller
    {
        private Modelo db = new Modelo();

        // GET: AnalisisFODA
        public ActionResult Index()
        {
            if (Session["PlanId"] == null)
                return RedirectToAction("Login", "Account");

            int planId = (int)Session["PlanId"];

            var analisis = db.AnalisisFODA.FirstOrDefault(a => a.PlanId == planId);
            if (analisis == null)
                analisis = new AnalisisFODA { PlanId = planId };

            return View(analisis);
        }

        [HttpPost]
        public ActionResult Index(AnalisisFODA model)
        {
            if (Session["PlanId"] == null)
                return RedirectToAction("Login", "Account");

            int planId = (int)Session["PlanId"];
            var existente = db.AnalisisFODA.FirstOrDefault(a => a.PlanId == planId);

            if (existente != null)
            {
                existente.Fortalezas = model.Fortalezas;
                existente.Debilidades = model.Debilidades;
                existente.Oportunidades = model.Oportunidades;
                existente.Amenazas = model.Amenazas;
                existente.UpdatedAt = DateTime.Now;
            }
            else
            {
                model.PlanId = planId;
                model.CreatedAt = DateTime.Now;
                db.AnalisisFODA.Add(model);
            }

            db.SaveChanges();
            return RedirectToAction("Index", "CadenaValor");
        }
    }
}