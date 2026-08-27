namespace SistemacotizacionprestamosAPI.Models
{
    public class Respuesta
    {
        public int IdRespuesta { get; set; }

        public int IdEncuesta { get; set; }

        public int IdPregunta { get; set; }

        public string? ValorTexto { get; set; }

        public int? ValorEntero { get; set; }

        public decimal? ValorDecimal { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public int? UsuarioModificacion { get; set; }
    }
}