using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class UsuarioRolRepository
    {
        private readonly DbContext _context;

        public UsuarioRolRepository(DbContext context)
        {
            _context = context;
        }

        public List<UsuarioRol> Listar(bool incluirInactivos = false)
        {
            List<UsuarioRol> lista = new();

            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ListarUsuariosRoles",
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

        public UsuarioRol? ObtenerPorId(int id)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ObtenerUsuarioRolPorId",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@UsuarioRolID",
                id);

            conn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return Mapear(dr);
            }

            return null;
        }

        public bool Insertar(UsuarioRol usuarioRol)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_InsertarUsuarioRol",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@UsuarioID",
                usuarioRol.IdUsuario);

            cmd.Parameters.AddWithValue(
                "@RolID",
                usuarioRol.IdRol);

            cmd.Parameters.AddWithValue(
                "@UsuarioCreacionID",
                usuarioRol.UsuarioCreacion.HasValue
                    ? usuarioRol.UsuarioCreacion.Value
                    : DBNull.Value);

            conn.Open();

            return cmd.ExecuteScalar() != null;
        }

        public bool Actualizar(UsuarioRol usuarioRol)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ActualizarUsuarioRol",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@UsuarioRolID",
                usuarioRol.IdUsuarioRol);

            cmd.Parameters.AddWithValue(
                "@UsuarioID",
                usuarioRol.IdUsuario);

            cmd.Parameters.AddWithValue(
                "@RolID",
                usuarioRol.IdRol);

            cmd.Parameters.AddWithValue(
                "@UsuarioModificacionID",
                usuarioRol.UsuarioModificacion.HasValue
                    ? usuarioRol.UsuarioModificacion.Value
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
                "sp_EliminarLogicoUsuarioRol",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@UsuarioRolID",
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
                "sp_RestaurarUsuarioRol",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@UsuarioRolID",
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

        private static UsuarioRol Mapear(SqlDataReader dr)
        {
            return new UsuarioRol
            {
                IdUsuarioRol = Convert.ToInt32(
                    dr["id_usuario_rol"]),

                IdUsuario = Convert.ToInt32(
                    dr["id_usuario"]),

                IdRol = Convert.ToInt32(
                    dr["id_rol"]),

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