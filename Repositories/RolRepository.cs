using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class RolRepository
    {
        private readonly DbContext _context;

        public RolRepository(DbContext context)
        {
            _context = context;
        }
        public List<Rol> Listar()
        {
            List<Rol> lista = new();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarRoles", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Rol rol = new Rol();

                    rol.IdRol = Convert.ToInt32(dr["id_rol"]);
                    rol.NombreRol = dr["nombre_rol"].ToString();
                    rol.Descripcion = dr["descripcion"].ToString();
                    rol.Activo = Convert.ToBoolean(dr["activo"]);

                    lista.Add(rol);
                }
            }

            return lista;
        }

        public Rol ObtenerPorId(int id)
        {
            Rol rol = new Rol();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerRolPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RolID", id);

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    rol.IdRol = Convert.ToInt32(dr["id_rol"]);
                    rol.NombreRol = dr["nombre_rol"].ToString();
                    rol.Descripcion = dr["descripcion"].ToString();
                    rol.Activo = Convert.ToBoolean(dr["activo"]);
                }
            }

            return rol;
        }

        public bool Insertar(Rol rol)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarRol", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NombreRol", rol.NombreRol);
                cmd.Parameters.AddWithValue("@Descripcion", rol.Descripcion ?? (object)DBNull.Value);

                conn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Actualizar(Rol rol)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarRol", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RolID", rol.IdRol);
                cmd.Parameters.AddWithValue("@NombreRol", rol.NombreRol);
                cmd.Parameters.AddWithValue("@Descripcion", rol.Descripcion ?? (object)DBNull.Value);

                conn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarLogicoRol", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RolID", id);

                conn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}