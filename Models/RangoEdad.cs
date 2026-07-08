namespace SistemacotizacionprestamosAPI.Models
{
    public class RangoEdad
    {
        public int IdRangoEdad { get; set; }
        public int EdadMinima { get; set; }
        public int EdadMaxima { get; set; }
        public string Descripcion { get; set; } = "";
        public bool Activo { get; set; }
    }
}