using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class EstadoCotizacionRepository
    {
        private readonly DbContext _context;

        public EstadoCotizacionRepository(DbContext context)
        {
            _context = context;
        }

        public List<EstadoCotizacion> Listar()
        {
            List<EstadoCotizacion> lista = new();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarEstadosCotizacion", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    EstadoCotizacion estado = new EstadoCotizacion();

                    estado.IdEstado = Convert.ToInt32(dr["id_estado"]);
                    estado.NombreEstado = dr["nombre_estado"].ToString()!;
                    estado.Descripcion = dr["descripcion"] as string;
                    estado.Activo = Convert.ToBoolean(dr["activo"]);

                    lista.Add(estado);
                }
            }

            return lista;
        }

        public EstadoCotizacion ObtenerPorId(int id)
        {
            EstadoCotizacion estado = new EstadoCotizacion();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerEstadoCotizacionPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EstadoID", id);

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    estado.IdEstado = Convert.ToInt32(dr["id_estado"]);
                    estado.NombreEstado = dr["nombre_estado"].ToString()!;
                    estado.Descripcion = dr["descripcion"] as string;
                    estado.Activo = Convert.ToBoolean(dr["activo"]);
                }
            }

            return estado;
        }

        public bool Insertar(EstadoCotizacion estado)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarEstadoCotizacion", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NombreEstado", estado.NombreEstado);
                cmd.Parameters.AddWithValue("@Descripcion", estado.Descripcion ?? (object)DBNull.Value);

                conn.Open();

                return cmd.ExecuteScalar() != null;
            }
        }

        public bool Actualizar(EstadoCotizacion estado)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarEstadoCotizacion", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EstadoID", estado.IdEstado);
                cmd.Parameters.AddWithValue("@NombreEstado", estado.NombreEstado);
                cmd.Parameters.AddWithValue("@Descripcion", estado.Descripcion ?? (object)DBNull.Value);

                conn.Open();

                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public bool Eliminar(int id)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarLogicoEstadoCotizacion", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EstadoID", id);

                conn.Open();

                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}