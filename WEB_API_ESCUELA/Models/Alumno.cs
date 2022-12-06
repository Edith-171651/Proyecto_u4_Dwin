using System;
using System.Collections.Generic;

namespace WEB_API_ESCUELA.Models
{
    public partial class Alumno
    {
        public decimal Id { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoP { get; set; }
        public string? ApellidoM { get; set; }
        public string? Grado { get; set; }
        public string? Grupo { get; set; }
    }
}
