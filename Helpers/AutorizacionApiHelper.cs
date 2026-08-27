using Microsoft.AspNetCore.Http;

namespace SistemacotizacionprestamosAPI.Helpers
{
    public static class AutorizacionApiHelper
    {
        public const string Administrador = "Administrador";
        public const string Encuestador = "Encuestador";
        public const string Consultor = "Consultor";

        public static string ObtenerRol(HttpRequest request)
        {
            if (request.Headers.TryGetValue(
                "X-Usuario-Rol",
                out var rol))
            {
                return rol.ToString();
            }

            return string.Empty;
        }

        public static int ObtenerIdUsuario(HttpRequest request)
        {
            if (request.Headers.TryGetValue(
                "X-Usuario-Id",
                out var id))
            {
                if (int.TryParse(id.ToString(), out int idUsuario))
                {
                    return idUsuario;
                }
            }

            return 0;
        }

        public static bool TieneRol(
            HttpRequest request,
            string rolEsperado)
        {
            string rol = ObtenerRol(request);

            return string.Equals(
                rol,
                rolEsperado,
                StringComparison.OrdinalIgnoreCase);
        }

        public static bool EsAdministrador(
            HttpRequest request)
        {
            return TieneRol(
                request,
                Administrador);
        }

        public static bool EsEncuestador(
            HttpRequest request)
        {
            return TieneRol(
                request,
                Encuestador);
        }

        public static bool EsConsultor(
            HttpRequest request)
        {
            return TieneRol(
                request,
                Consultor);
        }
    }
}