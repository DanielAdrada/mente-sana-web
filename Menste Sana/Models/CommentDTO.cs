using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Menste_Sana.Models
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }
        public bool Positivo { get; set; }
    }
}