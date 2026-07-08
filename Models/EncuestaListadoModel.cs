namespace SistemacotizacionprestamosAPI.Models
{
    public class EncuestaListadoModel
    {
        public int IdEncuesta { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}