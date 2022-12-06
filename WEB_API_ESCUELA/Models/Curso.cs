using System;
using System.Collections.Generic;

namespace WEB_API_ESCUELA.Models
{
    public partial class Curso
    {
        public decimal Id { get; set; }
        public string? Descripcion { get; set; }
        public decimal? IdProfesor { get; set; }

        public virtual Profesor? IdProfesorNavigation { get; set; }
    }
}
