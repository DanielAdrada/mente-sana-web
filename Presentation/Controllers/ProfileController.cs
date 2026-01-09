using Logic;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Menste_Sana.Models;

namespace Presentation.Controllers
{
    public class ProfileController : Controller
    {
        private UserLog userLogic = new UserLog();

        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "Login");

            string id = Session["UserId"].ToString();

            PerfilDTO perfil = userLogic.ObtenerPerfil(id);

            if (perfil == null)
            {
                perfil = new PerfilDTO
                {
                    Id = id,
                    Nombre = "",
                    Apellido = ""
                };
            }

            PerfilViewModel model = new PerfilViewModel
            {
                Id = perfil.Id,
                Nombre = perfil.Nombre,
                Apellido = perfil.Apellido
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PerfilViewModel model)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                bool ok = userLogic.GuardarPerfil(
                    model.Id,
                    model.Nombre,
                    model.Apellido
                );

                if (ok)
                    ViewBag.Mensaje = "Perfil guardado correctamente";
                else
                    ViewBag.Error = "No se pudo guardar el perfil";
            }

            return View(model);
        }

    }
}