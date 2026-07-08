namespace SistemacotizacionprestamosAPI.Models
{
    public class EstadoCotizacion
    {
        public int IdEstado { get; set; }

        public string NombreEstado { get; set; } = "";

        public string? Descripcion { get; set; }

        public bool Activo { get; set; }
    }
}