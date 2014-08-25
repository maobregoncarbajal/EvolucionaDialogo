using System;

namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeAdmin
    {
        public int IDAdmin { get; set; }
        public string CodigoAdmin { get; set; }
        public string ClaveAdmin { get; set; }
        public string NombreCompleto { get; set; }
        public string TipoAdmin { get; set; }
        public string CodigoPais { get; set; }
        public bool Estado { get; set; }
        public bool Admin { get; set; }
        public int? UsuarioCrea { get; set; }
        public DateTime? FechaCrea { get; set; }
        public int? UsuarioModi { get; set; }
        public DateTime? FechaModi { get; set; }
        public string NombrePais { get; set; }
        public string DescripcionAdmin { get; set; }
        public string ImagenPais { get; set; }
    }
}
