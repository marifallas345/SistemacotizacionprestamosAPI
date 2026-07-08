namespace SistemacotizacionprestamosAPI.Models
{
    public class Plazo
    {
        public int IdPlazo { get; set; }
        public int Meses { get; set; }
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}