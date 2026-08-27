namespace SistemacotizacionprestamosAPI.Models
{
    public class UsuarioRol
    {
        public int IdUsuarioRol { get; set; }

        public int IdUsuario { get; set; }

        public int IdRol { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public int? UsuarioModificacion { get; set; }
    }
}