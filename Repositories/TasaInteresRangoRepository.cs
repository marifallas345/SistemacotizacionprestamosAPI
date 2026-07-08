using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class TasaInteresRangoRepository
    {
        private readonly DbContext _context;

        public TasaInteresRangoRepository(DbContext context)
        {
            _context = context;
        }

        private TasaInteresRango Mapear(SqlDataReader dr)
        {
            return new TasaInteresRango
            {
                IdTasaRango = Convert.ToInt32(dr["id_tasa_rango"]),
                TasaMinima = Convert.ToDecimal(dr["tasa_minima"]),
                TasaMaxima = Convert.ToDecimal(dr["tasa_maxima"]),
                Descripcion = dr["descripcion"].ToString()!,
                Activo = Convert.ToBoolean(dr["activo"])
            };
        }

        public List<TasaInteresRango> Listar()
        {
            List<TasaInteresRango> lista = new();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarTasasInteresRango", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                    lista.Add(Mapear(dr));
            }

            return lista;
        }

        public TasaInteresRango ObtenerPorId(int id)
        {
            TasaInteresRango item = new TasaInteresRango();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerTasaInteresRangoPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TasaRangoID", id);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                    item = Mapear(dr);
            }

            return item;
        }

        public bool Insertar(TasaInteresRango item)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarTasaInteresRango", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TasaMinima", item.TasaMinima);
                cmd.Parameters.AddWithValue("@TasaMaxima", item.TasaMaxima);
                cmd.Parameters.AddWithValue("@Descripcion", item.Descripcion);

                conn.Open();
                return cmd.ExecuteScalar() != null;
            }
        }

        public bool Actualizar(TasaInteresRango item)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarTasaInteresRango", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TasaRangoID", item.IdTasaRango);
                cmd.Parameters.AddWithValue("@TasaMinima", item.TasaMinima);
                cmd.Parameters.AddWithValue("@TasaMaxima", item.TasaMaxima);
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
                SqlCommand cmd = new SqlCommand("sp_EliminarLogicoTasaInteresRango", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TasaRangoID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}