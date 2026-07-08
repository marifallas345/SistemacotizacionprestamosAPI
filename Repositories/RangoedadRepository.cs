using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class RangoEdadRepository
    {
        private readonly DbContext _context;

        public RangoEdadRepository(DbContext context)
        {
            _context = context;
        }

        private RangoEdad Mapear(SqlDataReader dr)
        {
            return new RangoEdad
            {
                IdRangoEdad = Convert.ToInt32(dr["id_rango_edad"]),
                EdadMinima = Convert.ToInt32(dr["edad_minima"]),
                EdadMaxima = Convert.ToInt32(dr["edad_maxima"]),
                Descripcion = dr["descripcion"].ToString()!,
                Activo = Convert.ToBoolean(dr["activo"])
            };
        }

        public List<RangoEdad> Listar()
        {
            List<RangoEdad> lista = new();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarRangosEdad", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                    lista.Add(Mapear(dr));
            }

            return lista;
        }

        public RangoEdad ObtenerPorId(int id)
        {
            RangoEdad item = new RangoEdad();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerRangoEdadPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RangoEdadID", id);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                    item = Mapear(dr);
            }

            return item;
        }

        public bool Insertar(RangoEdad item)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarRangoEdad", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EdadMinima", item.EdadMinima);
                cmd.Parameters.AddWithValue("@EdadMaxima", item.EdadMaxima);
                cmd.Parameters.AddWithValue("@Descripcion", item.Descripcion);

                conn.Open();
                return cmd.ExecuteScalar() != null;
            }
        }

        public bool Actualizar(RangoEdad item)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarRangoEdad", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RangoEdadID", item.IdRangoEdad);
                cmd.Parameters.AddWithValue("@EdadMinima", item.EdadMinima);
                cmd.Parameters.AddWithValue("@EdadMaxima", item.EdadMaxima);
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
                SqlCommand cmd = new SqlCommand("sp_EliminarLogicoRangoEdad", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RangoEdadID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}