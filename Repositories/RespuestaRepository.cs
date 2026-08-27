using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class RespuestaRepository
    {
        private readonly DbContext _context;

        public RespuestaRepository(DbContext context)
        {
            _context = context;
        }

        public List<Respuesta> Listar(bool incluirInactivos = false)
        {
            List<Respuesta> lista = new();

            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ListarRespuestas",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@IncluirInactivos",
                incluirInactivos);

            conn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(Mapear(dr));
            }

            return lista;
        }

        public Respuesta? ObtenerPorId(int id)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ObtenerRespuestaPorId",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@RespuestaID",
                id);

            conn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return Mapear(dr);
            }

            return null;
        }

        public bool Insertar(Respuesta respuesta)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_InsertarRespuesta",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@EncuestaID",
                respuesta.IdEncuesta);

            cmd.Parameters.AddWithValue(
                "@PreguntaID",
                respuesta.IdPregunta);

            cmd.Parameters.AddWithValue(
                "@ValorTexto",
                (object?)respuesta.ValorTexto ?? DBNull.Value);

            cmd.Parameters.AddWithValue(
                "@ValorEntero",
                respuesta.ValorEntero.HasValue
                    ? respuesta.ValorEntero.Value
                    : DBNull.Value);

            cmd.Parameters.AddWithValue(
                "@ValorDecimal",
                respuesta.ValorDecimal.HasValue
                    ? respuesta.ValorDecimal.Value
                    : DBNull.Value);

            cmd.Parameters.AddWithValue(
                "@UsuarioCreacionID",
                respuesta.UsuarioCreacion.HasValue
                    ? respuesta.UsuarioCreacion.Value
                    : DBNull.Value);

            conn.Open();

            return cmd.ExecuteScalar() != null;
        }

        public bool Actualizar(Respuesta respuesta)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ActualizarRespuesta",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@RespuestaID",
                respuesta.IdRespuesta);

            cmd.Parameters.AddWithValue(
                "@EncuestaID",
                respuesta.IdEncuesta);

            cmd.Parameters.AddWithValue(
                "@PreguntaID",
                respuesta.IdPregunta);

            cmd.Parameters.AddWithValue(
                "@ValorTexto",
                (object?)respuesta.ValorTexto ?? DBNull.Value);

            cmd.Parameters.AddWithValue(
                "@ValorEntero",
                respuesta.ValorEntero.HasValue
                    ? respuesta.ValorEntero.Value
                    : DBNull.Value);

            cmd.Parameters.AddWithValue(
                "@ValorDecimal",
                respuesta.ValorDecimal.HasValue
                    ? respuesta.ValorDecimal.Value
                    : DBNull.Value);

            cmd.Parameters.AddWithValue(
                "@UsuarioModificacionID",
                respuesta.UsuarioModificacion.HasValue
                    ? respuesta.UsuarioModificacion.Value
                    : DBNull.Value);

            conn.Open();

            cmd.ExecuteNonQuery();

            return true;
        }

        public bool Eliminar(
            int id,
            int? usuarioModificacion = null)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_EliminarLogicoRespuesta",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@RespuestaID",
                id);

            cmd.Parameters.AddWithValue(
                "@UsuarioModificacionID",
                usuarioModificacion.HasValue
                    ? usuarioModificacion.Value
                    : DBNull.Value);

            conn.Open();

            cmd.ExecuteNonQuery();

            return true;
        }

        public bool Restaurar(
            int id,
            int? usuarioModificacion = null)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_RestaurarRespuesta",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@RespuestaID",
                id);

            cmd.Parameters.AddWithValue(
                "@UsuarioModificacionID",
                usuarioModificacion.HasValue
                    ? usuarioModificacion.Value
                    : DBNull.Value);

            conn.Open();

            cmd.ExecuteNonQuery();

            return true;
        }

        private static Respuesta Mapear(SqlDataReader dr)
        {
            return new Respuesta
            {
                IdRespuesta = Convert.ToInt32(
                    dr["id_respuesta"]),

                IdEncuesta = Convert.ToInt32(
                    dr["id_encuesta"]),

                IdPregunta = Convert.ToInt32(
                    dr["id_pregunta"]),

                ValorTexto = dr["valor_texto"] == DBNull.Value
                    ? null
                    : dr["valor_texto"].ToString(),

                ValorEntero = dr["valor_entero"] == DBNull.Value
                    ? null
                    : Convert.ToInt32(
                        dr["valor_entero"]),

                ValorDecimal = dr["valor_decimal"] == DBNull.Value
                    ? null
                    : Convert.ToDecimal(
                        dr["valor_decimal"]),

                Activo = Convert.ToBoolean(
                    dr["activo"]),

                FechaCreacion = Convert.ToDateTime(
                    dr["fecha_creacion"]),

                UsuarioCreacion = dr["usuario_creacion"] == DBNull.Value
                    ? null
                    : Convert.ToInt32(
                        dr["usuario_creacion"]),

                FechaModificacion = dr["fecha_modificacion"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(
                        dr["fecha_modificacion"]),

                UsuarioModificacion = dr["usuario_modificacion"] == DBNull.Value
                    ? null
                    : Convert.ToInt32(
                        dr["usuario_modificacion"])
            };
        }
    }
}