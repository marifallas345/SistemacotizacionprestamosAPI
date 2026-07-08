namespace SistemacotizacionprestamosAPI.Models
{
    public class CapacidadPago
    {
        public int IdCapacidadPago { get; set; }
        public decimal MontoMinimo { get; set; }
        public decimal MontoMaximo { get; set; }
        public string Descripcion { get; set; } = "";
        public bool Activo { get; set; }
    }
}