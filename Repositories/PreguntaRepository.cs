using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class PreguntaRepository
    {
        private readonly DbContext _context;

        public PreguntaRepository(DbContext context)
        {
            _context = context;
        }

        public List<Pregunta> Listar(bool incluirInactivos = false)
        {
            List<Pregunta> lista = new();

            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ListarPreguntas",
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

        public Pregunta? ObtenerPorId(int id)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ObtenerPreguntaPorId",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@PreguntaID",
                id);

            conn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return Mapear(dr);
            }

            return null;
        }

        public bool Insertar(Pregunta pregunta)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_InsertarPregunta",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@Texto",
                pregunta.Texto);

            cmd.Parameters.AddWithValue(
                "@TipoControl",
                pregunta.TipoControl);

            cmd.Parameters.AddWithValue(
                "@CategoriaID",
                pregunta.IdCategoria);

            cmd.Parameters.AddWithValue(
                "@Orden",
                pregunta.Orden);

            cmd.Parameters.AddWithValue(
                "@Obligatoria",
                pregunta.Obligatoria);

            cmd.Parameters.AddWithValue(
                "@UsuarioCreacionID",
                pregunta.UsuarioCreacion.HasValue
                    ? pregunta.UsuarioCreacion.Value
                    : DBNull.Value);

            conn.Open();

            return cmd.ExecuteScalar() != null;
        }

        public bool Actualizar(Pregunta pregunta)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ActualizarPregunta",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@PreguntaID",
                pregunta.IdPregunta);

            cmd.Parameters.AddWithValue(
                "@Texto",
                pregunta.Texto);

            cmd.Parameters.AddWithValue(
                "@TipoControl",
                pregunta.TipoControl);

            cmd.Parameters.AddWithValue(
                "@CategoriaID",
                pregunta.IdCategoria);

            cmd.Parameters.AddWithValue(
                "@Orden",
                pregunta.Orden);

            cmd.Parameters.AddWithValue(
                "@Obligatoria",
                pregunta.Obligatoria);

            cmd.Parameters.AddWithValue(
                "@UsuarioModificacionID",
                pregunta.UsuarioModificacion.HasValue
                    ? pregunta.UsuarioModificacion.Value
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
                "sp_EliminarLogicoPregunta",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@PreguntaID",
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
                "sp_RestaurarPregunta",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@PreguntaID",
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

        private static Pregunta Mapear(SqlDataReader dr)
        {
            return new Pregunta
            {
                IdPregunta = Convert.ToInt32(
                    dr["id_pregunta"]),

                Texto = dr["texto"].ToString() ?? "",

                TipoControl = dr["tipo_control"].ToString() ?? "",

                IdCategoria = Convert.ToInt32(
                    dr["id_categoria"]),

                Orden = Convert.ToInt32(
                    dr["orden"]),

                Obligatoria = Convert.ToBoolean(
                    dr["obligatoria"]),

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