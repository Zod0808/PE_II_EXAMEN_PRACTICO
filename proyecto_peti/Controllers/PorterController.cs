using System;
using System.Linq;
using System.Web.Mvc;
using proyecto_peti.Models;

namespace proyecto_peti.Controllers
{
    public class PorterController : Controller
    {
        private Modelo db = new Modelo();

        public ActionResult Index()
        {
            if (Session["PlanId"] == null)
                return RedirectToAction("Login", "Account");

            int planId = (int)Session["PlanId"];

            var modelo = db.FuerzasPorter.FirstOrDefault(x => x.PlanId == planId);
            if (modelo == null)
            {
                modelo = new FuerzasPorter { PlanId = planId };
            }

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Index(FuerzasPorter model)
        {
            if (Session["PlanId"] == null)
                return RedirectToAction("Login", "Account");

            int planId = (int)Session["PlanId"];
            var existente = db.FuerzasPorter.FirstOrDefault(x => x.PlanId == planId);

            if (existente != null)
            {
                existente.AmenazaNuevos = model.AmenazaNuevos;
                existente.RivalidadCompetidores = model.RivalidadCompetidores;
                existente.PoderClientes = model.PoderClientes;
                existente.PoderProveedores = model.PoderProveedores;
                existente.AmenazaSustitutos = model.AmenazaSustitutos;
            }
            else
            {
                model.PlanId = planId;
                db.FuerzasPorter.Add(model);
            }

            db.SaveChanges();
            return RedirectToAction("Index", "PEST");
        }
    }
}
