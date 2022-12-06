using System;
using System.Collections.Generic;

namespace WEB_API_ESCUELA.Models
{
    public partial class Profesor
    {
        public Profesor()
        {
            Cursos = new HashSet<Curso>();
        }

        public decimal Id { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoP { get; set; }
        public string? ApellidoM { get; set; }
        public string? Direccion { get; set; }

        public virtual ICollection<Curso> Cursos { get; set; }
    }
}
