using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class MontoRangoRepository
    {
        private readonly DbContext _context;

        public MontoRangoRepository(DbContext context)
        {
            _context = context;
        }

        private MontoRango Mapear(SqlDataReader dr)
        {
            return new MontoRango
            {
                IdMontoRango = Convert.ToInt32(dr["id_monto_rango"]),
                MontoMinimo = Convert.ToDecimal(dr["monto_minimo"]),
                MontoMaximo = Convert.ToDecimal(dr["monto_maximo"]),
                Descripcion = dr["descripcion"].ToString()!,
                Activo = Convert.ToBoolean(dr["activo"])
            };
        }

        public List<MontoRango> Listar()
        {
            List<MontoRango> lista = new();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarMontosRango", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                    lista.Add(Mapear(dr));
            }

            return lista;
        }

        public MontoRango ObtenerPorId(int id)
        {
            MontoRango item = new MontoRango();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerMontoRangoPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MontoRangoID", id);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                    item = Mapear(dr);
            }

            return item;
        }

        public bool Insertar(MontoRango item)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarMontoRango", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MontoMinimo", item.MontoMinimo);
                cmd.Parameters.AddWithValue("@MontoMaximo", item.MontoMaximo);
                cmd.Parameters.AddWithValue("@Descripcion", item.Descripcion);

                conn.Open();
                return cmd.ExecuteScalar() != null;
            }
        }

        public bool Actualizar(MontoRango item)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarMontoRango", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MontoRangoID", item.IdMontoRango);
                cmd.Parameters.AddWithValue("@MontoMinimo", item.MontoMinimo);
                cmd.Parameters.AddWithValue("@MontoMaximo", item.MontoMaximo);
                cmd.Parameters.AddWithValue("@Descripcion", item.Descripcion);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public bool Eliminar(int id)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarLogicoMontoRango", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MontoRangoID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}