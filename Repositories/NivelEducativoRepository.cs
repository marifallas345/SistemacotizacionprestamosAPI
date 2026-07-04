using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class NivelEducativoRepository
    {
        private readonly DbContext _context;

        public NivelEducativoRepository(DbContext context)
        {
            _context = context;
        }

        public List<NivelEducativo> Listar()
        {
            List<NivelEducativo> lista = new();

            using SqlConnection conn = _context.CreateConnection();

            SqlCommand cmd = new SqlCommand("sp_ListarNivelesEducativos", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new NivelEducativo
                {
                    IdNivelEducativo = Convert.ToInt32(dr["id_nivel_educativo"]),
                    Nombre = dr["nombre"].ToString()!,
                    Activo = Convert.ToBoolean(dr["activo"])
                });
            }

            return lista;
        }

        public NivelEducativo ObtenerPorId(int id)
        {
            NivelEducativo nivel = new();

            using SqlConnection conn = _context.CreateConnection();

            SqlCommand cmd = new SqlCommand("sp_ObtenerNivelEducativoPorId", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NivelEducativoID", id);

            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                nivel.IdNivelEducativo = Convert.ToInt32(dr["id_nivel_educativo"]);
                nivel.Nombre = dr["nombre"].ToString()!;
                nivel.Activo = Convert.ToBoolean(dr["activo"]);
            }

            return nivel;
        }

        public bool Insertar(NivelEducativo nivel)
        {
            using SqlConnection conn = _context.CreateConnection();

            SqlCommand cmd = new SqlCommand("sp_InsertarNivelEducativo", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Nombre", nivel.Nombre);

            conn.Open();

            return cmd.ExecuteScalar() != null;
        }

        public bool Actualizar(NivelEducativo nivel)
        {
            using SqlConnection conn = _context.CreateConnection();

            SqlCommand cmd = new SqlCommand("sp_ActualizarNivelEducativo", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NivelEducativoID", nivel.IdNivelEducativo);
            cmd.Parameters.AddWithValue("@Nombre", nivel.Nombre);

            conn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Eliminar(int id)
        {
            using SqlConnection conn = _context.CreateConnection();

            SqlCommand cmd = new SqlCommand("sp_EliminarLogicoNivelEducativo", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NivelEducativoID", id);

            conn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}