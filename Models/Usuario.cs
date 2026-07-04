namespace SistemacotizacionprestamosAPI.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = "";

        public string HashPassword { get; set; } = "";

        public string Email { get; set; } = "";

        public string Nombre { get; set; } = "";

        public bool Activo { get; set; }

        public int IdRol { get; set; }

        public string NombreRol { get; set; } = "";
    }
}