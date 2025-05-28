using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using proyecto_peti.Models;

namespace proyecto_peti.Controllers
{
    public class AccountController : Controller
    {
        private Modelo db = new Modelo(); // Usa el DbContext generado

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users model)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = HashPassword(model.Password);

                var user = db.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == hashedPassword);

                if (user != null)
                {
                    // Buscar o crear plan asociado al usuario
                    var plan = db.PlanEstrategico.FirstOrDefault(p => p.UserId == user.Id);
                    if (plan == null)
                    {
                        plan = new PlanEstrategico
                        {
                            UserId = user.Id,
                            FechaCreacion = DateTime.Now
                        };
                        db.PlanEstrategico.Add(plan);
                        db.SaveChanges();
                    }

                    // Guardar el ID del plan en sesión
                    Session["PlanId"] = plan.Id;

                    // Iniciar sesión
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "Menu");
                }
                else
                {
                    ViewBag.Message = "Usuario o contraseña incorrectos";
                }
            }

            return View(model);
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users model)
        {
            if (ModelState.IsValid)
            {
                // Verifica si ya existe el usuario
                if (db.Users.Any(u => u.Username == model.Username))
                {
                    ViewBag.Message = "El nombre de usuario ya existe.";
                    return View(model);
                }

                model.Password = HashPassword(model.Password);
                db.Users.Add(model);
                db.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(model);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
