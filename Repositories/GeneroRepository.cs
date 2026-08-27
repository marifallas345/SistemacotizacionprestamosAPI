using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class GeneroRepository
    {
        private readonly DbContext _context;

        public GeneroRepository(DbContext context)
        {
            _context = context;
        }

        public List<Genero> Listar(bool incluirInactivos = false)
        {
            List<Genero> lista = new();

            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ListarGeneros",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@IncluirInactivos",
                incluirInactivos);

            conn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(Mapear(dr));
            }

            return lista;
        }

        public Genero? ObtenerPorId(int id)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ObtenerGeneroPorId",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GeneroID", id);

            conn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return Mapear(dr);
            }

            return null;
        }

        public bool Insertar(Genero genero)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_InsertarGenero",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@Nombre",
                genero.Nombre);

            conn.Open();

            return cmd.ExecuteScalar() != null;
        }

        public bool Actualizar(Genero genero)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ActualizarGenero",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@GeneroID",
                genero.IdGenero);

            cmd.Parameters.AddWithValue(
                "@Nombre",
                genero.Nombre);

            conn.Open();

            cmd.ExecuteNonQuery();

            return true;
        }

        public bool Eliminar(int id)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_EliminarLogicoGenero",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@GeneroID",
                id);

            conn.Open();

            cmd.ExecuteNonQuery();

            return true;
        }

        public bool Restaurar(int id)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_RestaurarGenero",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@GeneroID",
                id);

            conn.Open();

            cmd.ExecuteNonQuery();

            return true;
        }

        private static Genero Mapear(SqlDataReader dr)
        {
            return new Genero
            {
                IdGenero = Convert.ToInt32(dr["id_genero"]),
                Nombre = dr["nombre"].ToString() ?? "",
                Activo = Convert.ToBoolean(dr["activo"])
            };
        }
    }
}