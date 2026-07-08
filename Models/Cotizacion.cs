namespace SistemacotizacionprestamosAPI.Models
{
    public class Cotizacion
    {
        public int IdCotizacion { get; set; }

        public int IdCliente { get; set; }

        public int IdTipoPrestamo { get; set; }

        public int IdPlazo { get; set; }

        public int IdEstado { get; set; }

        public int? IdUsuario { get; set; }

        public decimal MontoSolicitado { get; set; }

        public decimal TasaInteresAplicada { get; set; }

        public decimal CuotaMensual { get; set; }

        public decimal MontoTotalIntereses { get; set; }

        public decimal MontoTotalPagar { get; set; }

        public DateTime FechaCotizacion { get; set; }

        public string? Observaciones { get; set; }

        public bool Activo { get; set; }
    }
}