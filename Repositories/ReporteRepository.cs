using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class ReporteRepository
    {
        private readonly DbContext _context;

        private readonly Dictionary<int, string> _procedimientos = new()
        {
            { 1, "sp_ReportePregunta1" },
            { 2, "sp_ReportePregunta2" },
            { 3, "sp_ReportePregunta3" },
            { 4, "sp_ReportePregunta4" },
            { 5, "sp_ReportePregunta5" },
            { 6, "sp_ReportePregunta6" },
            { 7, "sp_ReportePregunta7" },
            { 8, "sp_ReportePregunta8" },
            { 9, "sp_ReportePregunta9" },
            { 10, "sp_ReportePregunta10" },
            { 11, "sp_ReportePregunta11" },
            { 12, "sp_ReportePregunta12" },
            { 13, "sp_ReportePregunta13" },
            { 14, "sp_ReportePregunta14" },
            { 15, "sp_ReportePregunta15" },
            { 16, "sp_ReportePregunta16" },
            { 17, "sp_ReportePregunta17" },
            { 18, "sp_ReportePregunta18" },
            { 19, "sp_ReportePregunta19" },
            { 20, "sp_ReportePregunta20" },
            { 21, "sp_ReportePregunta21" },
            { 22, "sp_ReportePregunta22" },
            { 23, "sp_ReportePregunta23" },
            { 24, "sp_ReportePregunta24" },
            { 25, "sp_ReportePregunta25" },
            { 26, "sp_ReportePregunta26" },
            { 27, "sp_ReportePregunta27" },
            { 28, "sp_ReportePregunta28" },
            { 29, "sp_ReportePregunta29" },
            { 30, "sp_ReportePregunta30" }
        };

        public ReporteRepository(DbContext context)
        {
            _context = context;
        }

        public List<ReporteResultadoModel> EjecutarReporte(
            int numero,
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            if (!_procedimientos.TryGetValue(numero, out string? procedimiento))
            {
                throw new ArgumentException(
                    "El número de reporte no es válido.");
            }

            List<ReporteResultadoModel> lista = new();

            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                procedimiento,
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@IdRangoEdad",
                idRangoEdad);

            cmd.Parameters.AddWithValue(
                "@IdRangoIngresos",
                idRangoIngresos);

            cmd.Parameters.AddWithValue(
                "@IdGenero",
                idGenero);

            conn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ReporteResultadoModel resultado = new();

                for (int i = 0; i < dr.FieldCount; i++)
                {
                    resultado[dr.GetName(i)] =
                        dr.IsDBNull(i)
                            ? null
                            : dr.GetValue(i);
                }

                lista.Add(resultado);
            }

            return lista;
        }
    }
}