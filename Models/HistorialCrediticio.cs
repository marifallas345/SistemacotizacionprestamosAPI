namespace SistemacotizacionprestamosAPI.Models
{
    public class HistorialCrediticio
    {
        public int IdHistorial { get; set; }

        public bool TienePrestamosPrevios { get; set; }

        public bool HaMorado { get; set; }

        public string? Descripcion { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public int? UsuarioModificacion { get; set; }
    }
}