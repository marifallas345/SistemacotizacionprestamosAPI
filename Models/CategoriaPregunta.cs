namespace SistemacotizacionprestamosAPI.Models
{
    public class CategoriaPregunta
    {
        public int IdCategoria { get; set; }

        public string Nombre { get; set; } = "";

        public string? Descripcion { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public int? UsuarioModificacion { get; set; }
    }
}