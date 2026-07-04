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

        public List<Genero> Listar()
        {
            List<Genero> lista = new();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarGeneros", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Genero
                    {
                        IdGenero = Convert.ToInt32(dr["id_genero"]),
                        Nombre = dr["nombre"].ToString()!,
                        Activo = Convert.ToBoolean(dr["activo"])
                    });
                }
            }

            return lista;
        }
    }
}