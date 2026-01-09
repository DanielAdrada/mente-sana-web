using Menste_Sana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class StudentHomeViewModel
    {
        public PerfilDTO Perfil { get; set; }
        public List<CommentDTO> Comentarios { get; set; }
    }
}