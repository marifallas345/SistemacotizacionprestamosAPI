using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class OcupacionRepository
    {
        private readonly DbContext _context;

        public OcupacionRepository(DbContext context)
        {
            _context = context;
        }

        public List<Ocupacion> Listar()
        {
            List<Ocupacion> lista = new();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarOcupaciones", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Ocupacion
                    {
                        IdOcupacion = Convert.ToInt32(dr["id_ocupacion"]),
                        Nombre = dr["nombre"].ToString()!,
                        Activo = Convert.ToBoolean(dr["activo"])
                    });
                }
            }

            return lista;
        }
    }
}