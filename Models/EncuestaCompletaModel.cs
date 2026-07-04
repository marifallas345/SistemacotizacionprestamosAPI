using System.Collections.Generic;

namespace SistemacotizacionprestamosAPI.Models
{
    public class EncuestaCompletaModel
    {
        public string Nombre { get; set; } = "";

        public string Apellidos { get; set; } = "";

        public string Email { get; set; } = "";

        public string Telefono { get; set; } = "";

        public int IdGenero { get; set; }

        public int IdNivelEducativo { get; set; }

        public int IdRangoIngresos { get; set; }

        public int IdRangoEdad { get; set; }

        public int IdOcupacion { get; set; }

        public int? IdUsuario { get; set; }

        public string? IpOrigen { get; set; }

        public List<RespuestaModel> Respuestas { get; set; } = new();
    }
}