namespace SistemacotizacionprestamosAPI.Models
{
    public class Pregunta
    {
        public int IdPregunta { get; set; }

        public string Texto { get; set; } = "";

        public string TipoControl { get; set; } = "";

        public int IdCategoria { get; set; }

        public int Orden { get; set; }

        public bool Obligatoria { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public int? UsuarioModificacion { get; set; }
    }
}