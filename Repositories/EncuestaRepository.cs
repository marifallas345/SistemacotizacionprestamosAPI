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
                parametroXml.Value = xml.ToString();
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

        public int ContarEncuestasActivas()
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Encuestas WHERE activo = 1",
                    conn);

                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<EncuestaListadoModel> ListarEncuestas(bool incluirEliminados = false)
        {
            List<EncuestaListadoModel> lista = new List<EncuestaListadoModel>();

            using (SqlConnection conn = _context.CreateConnection())
            {
                string sql = @"
                    SELECT
                        e.id_encuesta,
                        c.nombre + ' ' + c.apellidos AS NombreCompleto,
                        c.email,
                        c.telefono,
                        e.fecha_registro,
                        e.activo
                    FROM Encuestas e
                    INNER JOIN Clientes c
                        ON e.id_cliente = c.id_cliente ";

                if (!incluirEliminados)
                {
                    sql += " WHERE e.activo = 1 ";
                }

                sql += " ORDER BY e.fecha_registro DESC ";

                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        EncuestaListadoModel encuesta = new EncuestaListadoModel
                        {
                            IdEncuesta = Convert.ToInt32(dr["id_encuesta"]),
                            NombreCompleto = dr["NombreCompleto"].ToString(),
                            Email = dr["email"].ToString(),
                            Telefono = dr["telefono"].ToString(),
                            FechaCreacion = Convert.ToDateTime(dr["fecha_registro"]),
                            Activo = Convert.ToBoolean(dr["activo"])
                        };

                        lista.Add(encuesta);
                    }
                }
            }

            return lista;
        }

        public bool EliminarEncuesta(int idEncuesta)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(@"
                    UPDATE Encuestas
                    SET activo = 0,
                        fecha_modificacion = GETDATE()
                    WHERE id_encuesta = @IdEncuesta", conn);

                cmd.Parameters.AddWithValue("@IdEncuesta", idEncuesta);

                conn.Open();

                int filas = cmd.ExecuteNonQuery();

                return filas > 0;
            }
        }

        public bool RestaurarEncuesta(int idEncuesta)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(@"
                    UPDATE Encuestas
                    SET activo = 1,
                        fecha_modificacion = GETDATE()
                    WHERE id_encuesta = @IdEncuesta", conn);

                cmd.Parameters.AddWithValue("@IdEncuesta", idEncuesta);

                conn.Open();

                int filas = cmd.ExecuteNonQuery();

                return filas > 0;
            }
        }

        public object BuscarEncuestaPorNombre(string nombre)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                string sql = @"
            SELECT TOP 1
                e.id_encuesta,
                c.nombre + ' ' + c.apellidos AS Cliente,
                e.activo
            FROM Encuestas e
            INNER JOIN Clientes c
                ON e.id_cliente = c.id_cliente
            WHERE c.nombre + ' ' + c.apellidos
                LIKE '%' + @Nombre + '%'
            ORDER BY e.fecha_registro DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Nombre", nombre);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return new
                        {
                            IdEncuesta = Convert.ToInt32(dr["id_encuesta"]),
                            Cliente = dr["Cliente"].ToString(),
                            Activo = Convert.ToBoolean(dr["activo"])
                        };
                    }
                }

                return null;
            }
        }

        public void RegistrarAuditoria(int idUsuario, string accion, string tabla, int idRegistro, string detalle, string ipOrigen)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Auditorias
                    (
                        id_usuario,
                        accion,
                        tabla_afectada,
                        id_registro_afectado,
                        detalle,
                        fecha_accion,
                        ip_origen
                    )
                    VALUES
                    (
                        @IdUsuario,
                        @Accion,
                        @TablaAfectada,
                        @IdRegistroAfectado,
                        @Detalle,
                        GETDATE(),
                        @IpOrigen
                    )", conn);

                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@Accion", accion);
                cmd.Parameters.AddWithValue("@TablaAfectada", tabla);
                cmd.Parameters.AddWithValue("@IdRegistroAfectado", idRegistro);
                cmd.Parameters.AddWithValue("@Detalle", detalle);
                cmd.Parameters.AddWithValue("@IpOrigen", ipOrigen ?? "");

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}