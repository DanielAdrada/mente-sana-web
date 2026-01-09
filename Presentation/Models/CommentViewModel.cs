using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class CommentViewModel
    {
        public int UserId { get; set; }
        public string Usuario { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }
    }
}