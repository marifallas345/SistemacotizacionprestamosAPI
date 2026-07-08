using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class CapacidadPagoRepository
    {
        private readonly DbContext _context;

        public CapacidadPagoRepository(DbContext context)
        {
            _context = context;
        }

        private CapacidadPago Mapear(SqlDataReader dr)
        {
            return new CapacidadPago
            {
                IdCapacidadPago = Convert.ToInt32(dr["id_capacidad_pago"]),
                MontoMinimo = Convert.ToDecimal(dr["monto_minimo"]),
                MontoMaximo = Convert.ToDecimal(dr["monto_maximo"]),
                Descripcion = dr["descripcion"].ToString()!,
                Activo = Convert.ToBoolean(dr["activo"])
            };
        }

        public List<CapacidadPago> Listar()
        {
            List<CapacidadPago> lista = new();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarCapacidadesPago", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                    lista.Add(Mapear(dr));
            }

            return lista;
        }

        public CapacidadPago ObtenerPorId(int id)
        {
            CapacidadPago item = new CapacidadPago();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerCapacidadPagoPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CapacidadPagoID", id);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                    item = Mapear(dr);
            }

            return item;
        }

        public bool Insertar(CapacidadPago item)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarCapacidadPago", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MontoMinimo", item.MontoMinimo);
                cmd.Parameters.AddWithValue("@MontoMaximo", item.MontoMaximo);
                cmd.Parameters.AddWithValue("@Descripcion", item.Descripcion);

                conn.Open();
                return cmd.ExecuteScalar() != null;
            }
        }

        public bool Actualizar(CapacidadPago item)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarCapacidadPago", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CapacidadPagoID", item.IdCapacidadPago);
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
                SqlCommand cmd = new SqlCommand("sp_EliminarLogicoCapacidadPago", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CapacidadPagoID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}
