using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class PlazoRepository
    {
        private readonly DbContext _context;

        public PlazoRepository(DbContext context)
        {
            _context = context;
        }

        private Plazo Mapear(SqlDataReader dr)
        {
            return new Plazo
            {
                IdPlazo = Convert.ToInt32(dr["id_plazo"]),
                Meses = Convert.ToInt32(dr["meses"]),
                Descripcion = dr["descripcion"] == DBNull.Value
                    ? ""
                    : dr["descripcion"].ToString()!,
                Activo = Convert.ToBoolean(dr["activo"])
            };
        }

        // ============================================================
        // LISTAR
        // ============================================================

        public List<Plazo> Listar(bool incluirInactivos = false)
        {
            List<Plazo> lista = new();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand("sp_ListarPlazos", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@IncluirInactivos",
                    incluirInactivos);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(Mapear(dr));
                    }
                }
            }

            return lista;
        }

        // ============================================================
        // OBTENER POR ID
        // ============================================================

        public Plazo? ObtenerPorId(int id)
        {
            Plazo? plazo = null;

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "sp_ObtenerPlazoPorId",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@PlazoID",
                    id);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        plazo = Mapear(dr);
                    }
                }
            }

            return plazo;
        }

        // ============================================================
        // INSERTAR
        // ============================================================

        public bool Insertar(Plazo plazo)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "sp_InsertarPlazo",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@Meses",
                    plazo.Meses);

                cmd.Parameters.AddWithValue(
                    "@Descripcion",
                    string.IsNullOrWhiteSpace(plazo.Descripcion)
                        ? DBNull.Value
                        : plazo.Descripcion);

                conn.Open();

                return cmd.ExecuteScalar() != null;
            }
        }

        // ============================================================
        // ACTUALIZAR
        // ============================================================

        public bool Actualizar(Plazo plazo)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "sp_ActualizarPlazo",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@PlazoID",
                    plazo.IdPlazo);

                cmd.Parameters.AddWithValue(
                    "@Meses",
                    plazo.Meses);

                cmd.Parameters.AddWithValue(
                    "@Descripcion",
                    string.IsNullOrWhiteSpace(plazo.Descripcion)
                        ? DBNull.Value
                        : plazo.Descripcion);

                conn.Open();

                cmd.ExecuteNonQuery();

                return true;
            }
        }

        // ============================================================
        // ELIMINAR LÓGICAMENTE
        // ============================================================

        public bool Eliminar(int id)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "sp_EliminarLogicoPlazo",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@PlazoID",
                    id);

                conn.Open();

                cmd.ExecuteNonQuery();

                return true;
            }
        }

        // ============================================================
        // RESTAURAR
        // ============================================================

        public bool Restaurar(int id)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "sp_RestaurarPlazo",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@PlazoID",
                    id);

                conn.Open();

                cmd.ExecuteNonQuery();

                return true;
            }
        }
    }
}