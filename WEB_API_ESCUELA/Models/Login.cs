using System;
using System.Collections.Generic;

namespace WEB_API_ESCUELA.Models
{
    public partial class Login
    {
        public decimal Id { get; set; }
        public string? Usuario { get; set; }
        public string? Contrasena { get; set; }
        public string? Tipo { get; set; }
    }
}
