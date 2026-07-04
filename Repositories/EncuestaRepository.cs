using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;
using System.Text;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class EncuestaRepository
    {
        private readonly DbContext _context;

        public EncuestaRepository(DbContext context)
        {
            _context = context;
        }

        public bool GuardarEncuestaCompleta(EncuestaCompletaModel encuesta)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_GuardarEncuestaCompleta", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                StringBuilder xml = new StringBuilder();

                xml.Append("<Respuestas>");

                foreach (var r in encuesta.Respuestas)
                {
                    xml.Append("<Respuesta>");

                    xml.Append($"<IdPregunta>{r.IdPregunta}</IdPregunta>");
                    xml.Append($"<ValorTexto>{r.ValorTexto ?? ""}</ValorTexto>");

                    xml.Append("</Respuesta>");
                }

                xml.Append("</Respuestas>");

                // ========= IMPRIMIR EL XML ==========
                string xmlGenerado = xml.ToString();

                Console.WriteLine("====================================");
                Console.WriteLine("XML GENERADO:");
                Console.WriteLine(xmlGenerado);
                Console.WriteLine("====================================");

                // Parámetros
                cmd.Parameters.AddWithValue("@Nombre", encuesta.Nombre);
                cmd.Parameters.AddWithValue("@Apellidos", encuesta.Apellidos);
                cmd.Parameters.AddWithValue("@Email", encuesta.Email);
                cmd.Parameters.AddWithValue("@Telefono", encuesta.Telefono);

                cmd.Parameters.AddWithValue("@IdGenero", encuesta.IdGenero);
                cmd.Parameters.AddWithValue("@IdNivelEducativo", encuesta.IdNivelEducativo);
                cmd.Parameters.AddWithValue("@IdRangoIngresos", encuesta.IdRangoIngresos);
                cmd.Parameters.AddWithValue("@IdRangoEdad", encuesta.IdRangoEdad);
                cmd.Parameters.AddWithValue("@IdOcupacion", encuesta.IdOcupacion);

                cmd.Parameters.AddWithValue("@IdUsuario", encuesta.IdUsuario ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IpOrigen", encuesta.IpOrigen ?? "");

                SqlParameter parametroXml = new SqlParameter("@RespuestasXml", SqlDbType.Xml);
                parametroXml.Value = xmlGenerado;

                cmd.Parameters.Add(parametroXml);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        int exito = Convert.ToInt32(dr["Exito"]);

                        if (exito == 1)
                            return true;

                        string mensaje = dr["Mensaje"].ToString() ?? "Error desconocido.";

                        throw new Exception("SQL dijo: " + mensaje);
                    }
                }

                return false;
            }
        }
    }
}