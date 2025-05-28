using System.Linq;
using System.Web.Mvc;
using proyecto_peti.Models;

namespace proyecto_peti.Controllers
{
    public class InformacionEmpresaController : Controller
    {
        private Modelo db = new Modelo();

        public ActionResult Index()
        {
            // Validar sesión activa
            if (Session["PlanId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int planId = (int)Session["PlanId"];

            var info = db.InformacionEmpresa.FirstOrDefault(i => i.PlanId == planId);
            if (info == null)
            {
                info = new InformacionEmpresa { PlanId = planId };
            }

            return View(info);
        }


        [HttpPost]
        public ActionResult Index(InformacionEmpresa model)
        {
            if (ModelState.IsValid)
            {
                var existente = db.InformacionEmpresa.FirstOrDefault(i => i.PlanId == model.PlanId);
                if (existente == null)
                {
                    db.InformacionEmpresa.Add(model);
                }
                else
                {
                    existente.NombreEmpresa = model.NombreEmpresa;
                    existente.Descripcion = model.Descripcion;
                }

                db.SaveChanges();
                return RedirectToAction("Index", "Mision");
            }

            return View(model);
        }
    }
}
