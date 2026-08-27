using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class CategoriaPreguntaRepository
    {
        private readonly DbContext _context;

        public CategoriaPreguntaRepository(DbContext context)
        {
            _context = context;
        }

        public List<CategoriaPregunta> Listar(bool incluirInactivos = false)
        {
            List<CategoriaPregunta> lista = new();

            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ListarCategoriasPregunta",
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

        public CategoriaPregunta? ObtenerPorId(int id)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ObtenerCategoriaPreguntaPorId",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@CategoriaID",
                id);

            conn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return Mapear(dr);
            }

            return null;
        }

        public bool Insertar(CategoriaPregunta categoria)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_InsertarCategoriaPregunta",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@Nombre",
                categoria.Nombre);

            cmd.Parameters.AddWithValue(
                "@Descripcion",
                (object?)categoria.Descripcion ?? DBNull.Value);

            cmd.Parameters.AddWithValue(
                "@UsuarioCreacionID",
                categoria.UsuarioCreacion.HasValue
                    ? categoria.UsuarioCreacion.Value
                    : DBNull.Value);

            conn.Open();

            return cmd.ExecuteScalar() != null;
        }

        public bool Actualizar(CategoriaPregunta categoria)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ActualizarCategoriaPregunta",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@CategoriaID",
                categoria.IdCategoria);

            cmd.Parameters.AddWithValue(
                "@Nombre",
                categoria.Nombre);

            cmd.Parameters.AddWithValue(
                "@Descripcion",
                (object?)categoria.Descripcion ?? DBNull.Value);

            cmd.Parameters.AddWithValue(
                "@UsuarioModificacionID",
                categoria.UsuarioModificacion.HasValue
                    ? categoria.UsuarioModificacion.Value
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
                "sp_EliminarLogicoCategoriaPregunta",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@CategoriaID",
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
                "sp_RestaurarCategoriaPregunta",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@CategoriaID",
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

        private static CategoriaPregunta Mapear(
            SqlDataReader dr)
        {
            return new CategoriaPregunta
            {
                IdCategoria = Convert.ToInt32(
                    dr["id_categoria"]),

                Nombre = dr["nombre"].ToString() ?? "",

                Descripcion = dr["descripcion"] == DBNull.Value
                    ? null
                    : dr["descripcion"].ToString(),

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