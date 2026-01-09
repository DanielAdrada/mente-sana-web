using Logic;
using Menste_Sana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class CommunityController : Controller
    {
        CommentLog commentLog = new CommentLog();

        // GET: Community
        public ActionResult Index()
        {
            // Validar sesión
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "Login");

            List<CommentDTO> comentarios = commentLog.ObtenerComentarios();
            return View(comentarios);
        }

        // POST: Community/Create
        [HttpPost]
        public ActionResult Create(string contenido)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "Login");

            string usuId = Session["UserId"].ToString();

            commentLog.AgregarComentario(usuId, contenido);

            return RedirectToAction("Index");
        }
    }
}