namespace SistemacotizacionprestamosAPI.Models
{
    public class DetalleAmortizacion
    {
        public int IdDetalleAmortizacion { get; set; }

        public int IdCotizacion { get; set; }

        public int NumeroCuota { get; set; }

        public decimal MontoCapital { get; set; }

        public decimal MontoInteres { get; set; }

        public decimal MontoCuota { get; set; }

        public decimal SaldoPendiente { get; set; }

        public bool Activo { get; set; }
    }
}
