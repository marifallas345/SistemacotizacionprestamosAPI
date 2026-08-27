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

        // ============================================================
        // LISTAR
        // ============================================================

        public List<RangoIngreso> Listar(bool incluirInactivos = false)
        {
            List<RangoIngreso> lista = new();

            using SqlConnection conn = _context.CreateConnection();

            SqlCommand cmd =
                new SqlCommand("sp_ListarRangosIngresos", conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@IncluirInactivos",
                incluirInactivos);

            conn.Open();

            using SqlDataReader dr =
                cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new RangoIngreso
                {
                    IdRangoIngresos =
                        Convert.ToInt32(
                            dr["id_rango_ingresos"]),

                    MontoMinimo =
                        Convert.ToDecimal(
                            dr["monto_minimo"]),

                    MontoMaximo =
                        Convert.ToDecimal(
                            dr["monto_maximo"]),

                    Descripcion =
                        dr["descripcion"].ToString() ?? "",

                    Activo =
                        Convert.ToBoolean(
                            dr["activo"])
                });
            }

            return lista;
        }


        // ============================================================
        // OBTENER POR ID
        // ============================================================

        public RangoIngreso ObtenerPorId(int id)
        {
            RangoIngreso? rango = null;

            using SqlConnection conn =
                _context.CreateConnection();

            SqlCommand cmd =
                new SqlCommand(
                    "sp_ObtenerRangoIngresosPorId",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@RangoIngresosID",
                id);

            conn.Open();

            using SqlDataReader dr =
                cmd.ExecuteReader();

            if (dr.Read())
            {
                rango = new RangoIngreso
                {
                    IdRangoIngresos =
                        Convert.ToInt32(
                            dr["id_rango_ingresos"]),

                    MontoMinimo =
                        Convert.ToDecimal(
                            dr["monto_minimo"]),

                    MontoMaximo =
                        Convert.ToDecimal(
                            dr["monto_maximo"]),

                    Descripcion =
                        dr["descripcion"].ToString() ?? "",

                    Activo =
                        Convert.ToBoolean(
                            dr["activo"])
                };
            }

            return rango;
        }


        // ============================================================
        // INSERTAR
        // ============================================================

        public bool Insertar(RangoIngreso rango)
        {
            using SqlConnection conn =
                _context.CreateConnection();

            SqlCommand cmd =
                new SqlCommand(
                    "sp_InsertarRangoIngresos",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@MontoMinimo",
                rango.MontoMinimo);

            cmd.Parameters.AddWithValue(
                "@MontoMaximo",
                rango.MontoMaximo);

            cmd.Parameters.AddWithValue(
                "@Descripcion",
                rango.Descripcion);

            conn.Open();

            return cmd.ExecuteScalar() != null;
        }


        // ============================================================
        // ACTUALIZAR
        // ============================================================

        public bool Actualizar(RangoIngreso rango)
        {
            using SqlConnection conn =
                _context.CreateConnection();

            SqlCommand cmd =
                new SqlCommand(
                    "sp_ActualizarRangoIngresos",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@RangoIngresosID",
                rango.IdRangoIngresos);

            cmd.Parameters.AddWithValue(
                "@MontoMinimo",
                rango.MontoMinimo);

            cmd.Parameters.AddWithValue(
                "@MontoMaximo",
                rango.MontoMaximo);

            cmd.Parameters.AddWithValue(
                "@Descripcion",
                rango.Descripcion);

            conn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }


        // ============================================================
        // ELIMINAR LÓGICAMENTE
        // ============================================================

        public bool Eliminar(int id)
        {
            using SqlConnection conn =
                _context.CreateConnection();

            SqlCommand cmd =
                new SqlCommand(
                    "sp_EliminarLogicoRangoIngresos",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@RangoIngresosID",
                id);

            conn.Open();

            return cmd.ExecuteNonQuery() != 0;
        }


        // ============================================================
        // RESTAURAR
        // ============================================================

        public bool Restaurar(int id)
        {
            using SqlConnection conn =
                _context.CreateConnection();

            SqlCommand cmd =
                new SqlCommand(
                    "sp_RestaurarRangoIngresos",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@RangoIngresosID",
                id);

            conn.Open();

            return cmd.ExecuteNonQuery() != 0;
        }
    }
}