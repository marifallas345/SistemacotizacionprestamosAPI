using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class RangoIngresoRepository
    {
        private readonly DbContext _context;

        public RangoIngresoRepository(DbContext context)
        {
            _context = context;
        }

        public List<RangoIngreso> Listar()
        {
            List<RangoIngreso> lista = new();

            using SqlConnection conn = _context.CreateConnection();

            SqlCommand cmd = new SqlCommand("sp_ListarRangosIngresos", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new RangoIngreso
                {
                    IdRangoIngresos = Convert.ToInt32(dr["id_rango_ingresos"]),
                    MontoMinimo = Convert.ToDecimal(dr["monto_minimo"]),
                    MontoMaximo = Convert.ToDecimal(dr["monto_maximo"]),
                    Descripcion = dr["descripcion"].ToString()!,
                    Activo = Convert.ToBoolean(dr["activo"])
                });
            }

            return lista;
        }

        public RangoIngreso ObtenerPorId(int id)
        {
            RangoIngreso rango = new();

            using SqlConnection conn = _context.CreateConnection();

            SqlCommand cmd = new SqlCommand("sp_ObtenerRangoIngresosPorId", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RangoIngresosID", id);

            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                rango.IdRangoIngresos = Convert.ToInt32(dr["id_rango_ingresos"]);
                rango.MontoMinimo = Convert.ToDecimal(dr["monto_minimo"]);
                rango.MontoMaximo = Convert.ToDecimal(dr["monto_maximo"]);
                rango.Descripcion = dr["descripcion"].ToString()!;
                rango.Activo = Convert.ToBoolean(dr["activo"]);
            }

            return rango;
        }

        public bool Insertar(RangoIngreso rango)
        {
            using SqlConnection conn = _context.CreateConnection();

            SqlCommand cmd = new SqlCommand("sp_InsertarRangoIngresos", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MontoMinimo", rango.MontoMinimo);
            cmd.Parameters.AddWithValue("@MontoMaximo", rango.MontoMaximo);
            cmd.Parameters.AddWithValue("@Descripcion", rango.Descripcion);

            conn.Open();

            return cmd.ExecuteScalar() != null;
        }

        public bool Actualizar(RangoIngreso rango)
        {
            using SqlConnection conn = _context.CreateConnection();

            SqlCommand cmd = new SqlCommand("sp_ActualizarRangoIngresos", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RangoIngresosID", rango.IdRangoIngresos);
            cmd.Parameters.AddWithValue("@MontoMinimo", rango.MontoMinimo);
            cmd.Parameters.AddWithValue("@MontoMaximo", rango.MontoMaximo);
            cmd.Parameters.AddWithValue("@Descripcion", rango.Descripcion);

            conn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Eliminar(int id)
        {
            using SqlConnection conn = _context.CreateConnection();

            SqlCommand cmd = new SqlCommand("sp_EliminarLogicoRangoIngresos", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RangoIngresosID", id);

            conn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}