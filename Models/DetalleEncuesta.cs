namespace SistemacotizacionprestamosAPI.Models
{
    public class DetalleEncuesta
    {
        public int IdEncuesta { get; set; }
        public int IdCliente { get; set; }

        public string NombreCliente { get; set; } = "";
        public string Email { get; set; } = "";
        public string Telefono { get; set; } = "";

        public int? IdUsuario { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string IpOrigen { get; set; } = "";

        public bool Activo { get; set; }

        public List<DetalleRespuestaEncuesta> Respuestas { get; set; }
            = new List<DetalleRespuestaEncuesta>();
    }

    public class DetalleRespuestaEncuesta
    {
        public int IdPregunta { get; set; }

        public string Pregunta { get; set; } = "";

        public string TipoControl { get; set; } = "";

        public int Orden { get; set; }

        public string ValorTexto { get; set; } = "";

        public int? ValorEntero { get; set; }

        public decimal? ValorDecimal { get; set; }
    }
}