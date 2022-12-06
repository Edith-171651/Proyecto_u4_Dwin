using System;
using System.Collections.Generic;

namespace WEB_API_ESCUELA.Models
{
    public partial class CursoAlumno
    {
        public decimal IdCurso { get; set; }
        public decimal IdAlumno { get; set; }

        public virtual Alumno IdAlumnoNavigation { get; set; } = null!;
        public virtual Curso IdCursoNavigation { get; set; } = null!;
    }
}
