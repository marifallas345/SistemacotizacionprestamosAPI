namespace SistemacotizacionprestamosAPI.Models
{
    public class TipoPrestamo
    {
        public int IdTipoPrestamo { get; set; }
        public string Nombre { get; set; } = "";
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
