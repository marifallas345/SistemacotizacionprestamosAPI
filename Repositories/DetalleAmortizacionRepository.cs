using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class DetalleAmortizacionRepository
    {
        private readonly DbContext _context;

        public DetalleAmortizacionRepository(DbContext context)
        {
            _context = context;
        }

        private DetalleAmortizacion Mapear(SqlDataReader dr)
        {
            DetalleAmortizacion detalle = new DetalleAmortizacion();

            detalle.IdDetalleAmortizacion = Convert.ToInt32(dr["id_detalle_amortizacion"]);
            detalle.IdCotizacion = Convert.ToInt32(dr["id_cotizacion"]);
            detalle.NumeroCuota = Convert.ToInt32(dr["numero_cuota"]);
            detalle.MontoCapital = Convert.ToDecimal(dr["monto_capital"]);
            detalle.MontoInteres = Convert.ToDecimal(dr["monto_interes"]);
            detalle.MontoCuota = Convert.ToDecimal(dr["monto_cuota"]);
            detalle.SaldoPendiente = Convert.ToDecimal(dr["saldo_pendiente"]);
            detalle.Activo = Convert.ToBoolean(dr["activo"]);

            return detalle;
        }

        public List<DetalleAmortizacion> Listar()
        {
            List<DetalleAmortizacion> lista = new();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarDetalleAmortizacion", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(Mapear(dr));
                }
            }

            return lista;
        }

        public List<DetalleAmortizacion> ListarPorCotizacion(int idCotizacion)
        {
            List<DetalleAmortizacion> lista = new();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarDetalleAmortizacionPorCotizacion", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdCotizacion", idCotizacion);

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(Mapear(dr));
                }
            }

            return lista;
        }

        public DetalleAmortizacion? ObtenerPorId(int id)
        {
            DetalleAmortizacion? detalle = null;

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerDetalleAmortizacionPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DetalleAmortizacionID", id);

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    detalle = Mapear(dr);
                }
            }

            return detalle;
        }

        public bool Insertar(DetalleAmortizacion detalle)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarDetalleAmortizacion", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdCotizacion", detalle.IdCotizacion);
                cmd.Parameters.AddWithValue("@NumeroCuota", detalle.NumeroCuota);
                cmd.Parameters.AddWithValue("@MontoCapital", detalle.MontoCapital);
                cmd.Parameters.AddWithValue("@MontoInteres", detalle.MontoInteres);
                cmd.Parameters.AddWithValue("@MontoCuota", detalle.MontoCuota);
                cmd.Parameters.AddWithValue("@SaldoPendiente", detalle.SaldoPendiente);

                conn.Open();

                return cmd.ExecuteScalar() != null;
            }
        }

        public bool Actualizar(DetalleAmortizacion detalle)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarDetalleAmortizacion", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DetalleAmortizacionID", detalle.IdDetalleAmortizacion);
                cmd.Parameters.AddWithValue("@MontoCapital", detalle.MontoCapital);
                cmd.Parameters.AddWithValue("@MontoInteres", detalle.MontoInteres);
                cmd.Parameters.AddWithValue("@MontoCuota", detalle.MontoCuota);
                cmd.Parameters.AddWithValue("@SaldoPendiente", detalle.SaldoPendiente);

                conn.Open();

                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public bool Eliminar(int id)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarLogicoDetalleAmortizacion", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DetalleAmortizacionID", id);

                conn.Open();

                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}