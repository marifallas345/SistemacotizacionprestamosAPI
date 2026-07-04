namespace SistemacotizacionprestamosAPI.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }

        public string Nombre { get; set; } = "";

        public string Apellidos { get; set; } = "";

        public string Email { get; set; } = "";

        public string Telefono { get; set; } = "";

        public int IdGenero { get; set; }

        public int IdNivelEducativo { get; set; }

        public int IdRangoIngresos { get; set; }

        public int IdRangoEdad { get; set; }

        public int IdOcupacion { get; set; }

        public bool Activo { get; set; }
    }
}