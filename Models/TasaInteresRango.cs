namespace SistemacotizacionprestamosAPI.Models
{
    public class TasaInteresRango
    {
        public int IdTasaRango { get; set; }
        public decimal TasaMinima { get; set; }
        public decimal TasaMaxima { get; set; }
        public string Descripcion { get; set; } = "";
        public bool Activo { get; set; }
    }
}