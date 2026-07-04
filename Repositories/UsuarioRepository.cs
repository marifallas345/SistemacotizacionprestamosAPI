using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class UsuarioRepository
    {
        private readonly DbContext _context;

        public UsuarioRepository(DbContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            List<Usuario> lista = new();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarUsuarios", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Usuario usuario = new Usuario();

                    usuario.IdUsuario = Convert.ToInt32(dr["id_usuario"]);
                    usuario.NombreUsuario = dr["nombre_usuario"].ToString()!;
                    usuario.HashPassword = dr["hash_password"].ToString()!;
                    usuario.Email = dr["email"].ToString()!;
                    usuario.Nombre = dr["nombre"].ToString()!;
                    usuario.Activo = Convert.ToBoolean(dr["activo"]);

                    lista.Add(usuario);
                }
            }

            return lista;
        }

        public Usuario ObtenerPorId(int id)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerUsuarioPorId", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UsuarioID", id);

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    usuario.IdUsuario = Convert.ToInt32(dr["id_usuario"]);
                    usuario.NombreUsuario = dr["nombre_usuario"].ToString();
                    usuario.HashPassword = dr["hash_password"].ToString();
                    usuario.Email = dr["email"].ToString();
                    usuario.Nombre = dr["nombre"].ToString();
                    usuario.Activo = Convert.ToBoolean(dr["activo"]);
                }
            }

            return usuario;
        }

        public bool Insertar(Usuario usuario)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarUsuario", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("@HashPassword", usuario.HashPassword);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);

                conn.Open();

                return cmd.ExecuteScalar() != null;
            }
        }

        public bool Actualizar(Usuario usuario)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarUsuario", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UsuarioID", usuario.IdUsuario);
                cmd.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("@HashPassword", usuario.HashPassword);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);

                conn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarLogicoUsuario", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UsuarioID", id);

                conn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public Usuario? Login(string nombreUsuario, string password)
        {
            Usuario? usuario = null;

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_Login", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

                //Convierte la contraseña a SHA256 antes de enviarla al SP
                cmd.Parameters.AddWithValue("@HashPassword",
                    PasswordHelper.GenerarHash(password));

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    usuario = new Usuario();

                    usuario.IdUsuario = Convert.ToInt32(dr["id_usuario"]);
                    usuario.NombreUsuario = dr["nombre_usuario"].ToString()!;
                    usuario.Nombre = dr["nombre"].ToString()!;
                    usuario.Email = dr["email"].ToString()!;
                    usuario.IdRol = Convert.ToInt32(dr["id_rol"]);
                    usuario.NombreRol = dr["nombre_rol"].ToString()!;
                }
            }

            return usuario;
        }
    }
 }