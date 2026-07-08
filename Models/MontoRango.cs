namespace SistemacotizacionprestamosAPI.Models
{
    public class MontoRango
    {
        public int IdMontoRango { get; set; }
        public decimal MontoMinimo { get; set; }
        public decimal MontoMaximo { get; set; }
        public string Descripcion { get; set; } = "";
        public bool Activo { get; set; }
    }
}