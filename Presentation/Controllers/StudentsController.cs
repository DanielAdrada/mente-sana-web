using Logic;
using Menste_Sana.Models;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class StudentsController : Controller
    {
        public ActionResult Index()
        {
            if(Session["UserId"] == null)
                return RedirectToAction("Index", "Login");

            string id = Session["UserId"].ToString();

            UserLog userLog = new UserLog();
            CommentLog commentLog = new CommentLog();

            var model = new StudentHomeViewModel
            {
                Perfil = userLog.ObtenerPerfil(id),
                Comentarios = commentLog.ObtenerComentariosPositivos()
            };


            return View(model);
        }
    }
}