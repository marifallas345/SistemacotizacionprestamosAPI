namespace SistemacotizacionprestamosAPI.Models
{
    public class AuditoriaModel
    {
        public int IdAuditoria { get; set; }

        public int IdUsuario { get; set; }

        public string Accion { get; set; } = "";

        public string TablaAfectada { get; set; } = "";

        public int IdRegistroAfectado { get; set; }

        public string Detalle { get; set; } = "";

        public DateTime FechaAccion { get; set; }

        public string IpOrigen { get; set; } = "";
    }
}