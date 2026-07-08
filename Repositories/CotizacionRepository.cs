using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class CotizacionRepository
    {
        private readonly DbContext _context;

        public CotizacionRepository(DbContext context)
        {
            _context = context;
        }

        private Cotizacion Mapear(SqlDataReader dr)
        {
            Cotizacion cotizacion = new Cotizacion();

            cotizacion.IdCotizacion = Convert.ToInt32(dr["id_cotizacion"]);
            cotizacion.IdCliente = Convert.ToInt32(dr["id_cliente"]);
            cotizacion.IdTipoPrestamo = Convert.ToInt32(dr["id_tipo_prestamo"]);
            cotizacion.IdPlazo = Convert.ToInt32(dr["id_plazo"]);
            cotizacion.IdEstado = Convert.ToInt32(dr["id_estado"]);
            cotizacion.IdUsuario = dr["id_usuario"] == DBNull.Value ? null : Convert.ToInt32(dr["id_usuario"]);
            cotizacion.MontoSolicitado = Convert.ToDecimal(dr["monto_solicitado"]);
            cotizacion.TasaInteresAplicada = Convert.ToDecimal(dr["tasa_interes_aplicada"]);
            cotizacion.CuotaMensual = Convert.ToDecimal(dr["cuota_mensual"]);
            cotizacion.MontoTotalIntereses = Convert.ToDecimal(dr["monto_total_intereses"]);
            cotizacion.MontoTotalPagar = Convert.ToDecimal(dr["monto_total_pagar"]);
            cotizacion.FechaCotizacion = Convert.ToDateTime(dr["fecha_cotizacion"]);
            cotizacion.Observaciones = dr["observaciones"] as string;
            cotizacion.Activo = Convert.ToBoolean(dr["activo"]);

            return cotizacion;
        }

        public List<Cotizacion> Listar()
        {
            List<Cotizacion> lista = new();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarCotizaciones", conn);

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

        public Cotizacion? ObtenerPorId(int id)
        {
            Cotizacion? cotizacion = null;

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerCotizacionPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CotizacionID", id);

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    cotizacion = Mapear(dr);
                }
            }

            return cotizacion;
        }

        public int Insertar(Cotizacion cotizacion)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarCotizacion", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdCliente", cotizacion.IdCliente);
                cmd.Parameters.AddWithValue("@IdTipoPrestamo", cotizacion.IdTipoPrestamo);
                cmd.Parameters.AddWithValue("@IdPlazo", cotizacion.IdPlazo);
                cmd.Parameters.AddWithValue("@IdEstado", cotizacion.IdEstado == 0 ? 1 : cotizacion.IdEstado);
                cmd.Parameters.AddWithValue("@IdUsuario", (object?)cotizacion.IdUsuario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MontoSolicitado", cotizacion.MontoSolicitado);
                cmd.Parameters.AddWithValue("@TasaInteresAplicada", cotizacion.TasaInteresAplicada);
                cmd.Parameters.AddWithValue("@CuotaMensual", cotizacion.CuotaMensual);
                cmd.Parameters.AddWithValue("@MontoTotalIntereses", cotizacion.MontoTotalIntereses);
                cmd.Parameters.AddWithValue("@MontoTotalPagar", cotizacion.MontoTotalPagar);
                cmd.Parameters.AddWithValue("@Observaciones", cotizacion.Observaciones ?? (object)DBNull.Value);

                conn.Open();

                object resultado = cmd.ExecuteScalar()!;

                return Convert.ToInt32(resultado);
            }
        }

        public bool Actualizar(Cotizacion cotizacion)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarCotizacion", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CotizacionID", cotizacion.IdCotizacion);
                cmd.Parameters.AddWithValue("@IdEstado", cotizacion.IdEstado);
                cmd.Parameters.AddWithValue("@MontoSolicitado", cotizacion.MontoSolicitado);
                cmd.Parameters.AddWithValue("@TasaInteresAplicada", cotizacion.TasaInteresAplicada);
                cmd.Parameters.AddWithValue("@CuotaMensual", cotizacion.CuotaMensual);
                cmd.Parameters.AddWithValue("@MontoTotalIntereses", cotizacion.MontoTotalIntereses);
                cmd.Parameters.AddWithValue("@MontoTotalPagar", cotizacion.MontoTotalPagar);
                cmd.Parameters.AddWithValue("@Observaciones", cotizacion.Observaciones ?? (object)DBNull.Value);

                conn.Open();

                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public bool Eliminar(int id)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarLogicoCotizacion", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CotizacionID", id);

                conn.Open();

                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}