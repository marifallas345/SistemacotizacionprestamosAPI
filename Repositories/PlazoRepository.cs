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
                Descripcion = dr["descripcion"] as string,
                Activo = Convert.ToBoolean(dr["activo"])
            };
        }

        public List<Plazo> Listar()
        {
            List<Plazo> lista = new();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarPlazos", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                    lista.Add(Mapear(dr));
            }

            return lista;
        }

        public Plazo ObtenerPorId(int id)
        {
            Plazo item = new Plazo();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerPlazoPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PlazoID", id);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                    item = Mapear(dr);
            }

            return item;
        }

        public bool Insertar(Plazo item)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarPlazo", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Meses", item.Meses);
                cmd.Parameters.AddWithValue("@Descripcion", item.Descripcion ?? (object)DBNull.Value);

                conn.Open();
                return cmd.ExecuteScalar() != null;
            }
        }

        public bool Actualizar(Plazo item)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarPlazo", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PlazoID", item.IdPlazo);
                cmd.Parameters.AddWithValue("@Meses", item.Meses);
                cmd.Parameters.AddWithValue("@Descripcion", item.Descripcion ?? (object)DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public bool Eliminar(int id)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarLogicoPlazo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PlazoID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}