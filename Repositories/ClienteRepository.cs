using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class ClienteRepository
    {
        private readonly DbContext _context;

        public ClienteRepository(DbContext context)
        {
            _context = context;
        }

        public List<Cliente> Listar()
        {
            List<Cliente> lista = new();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarClientes", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Cliente
                    {
                        IdCliente = Convert.ToInt32(dr["id_cliente"]),
                        Nombre = dr["nombre"].ToString()!,
                        Apellidos = dr["apellidos"].ToString()!,
                        Email = dr["email"].ToString()!,
                        Telefono = dr["telefono"].ToString()!,
                        IdGenero = Convert.ToInt32(dr["id_genero"]),
                        IdNivelEducativo = Convert.ToInt32(dr["id_nivel_educativo"]),
                        IdRangoIngresos = Convert.ToInt32(dr["id_rango_ingresos"]),
                        IdRangoEdad = Convert.ToInt32(dr["id_rango_edad"]),
                        IdOcupacion = Convert.ToInt32(dr["id_ocupacion"]),
                        Activo = Convert.ToBoolean(dr["activo"])
                    });
                }
            }

            return lista;
        }

        public Cliente ObtenerPorId(int id)
        {
            Cliente cliente = new Cliente();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerClientePorId", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClienteID", id);

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    cliente.IdCliente = Convert.ToInt32(dr["id_cliente"]);
                    cliente.Nombre = dr["nombre"].ToString()!;
                    cliente.Apellidos = dr["apellidos"].ToString()!;
                    cliente.Email = dr["email"].ToString()!;
                    cliente.Telefono = dr["telefono"].ToString()!;
                    cliente.IdGenero = Convert.ToInt32(dr["id_genero"]);
                    cliente.IdNivelEducativo = Convert.ToInt32(dr["id_nivel_educativo"]);
                    cliente.IdRangoIngresos = Convert.ToInt32(dr["id_rango_ingresos"]);
                    cliente.IdRangoEdad = Convert.ToInt32(dr["id_rango_edad"]);
                    cliente.IdOcupacion = Convert.ToInt32(dr["id_ocupacion"]);
                    cliente.Activo = Convert.ToBoolean(dr["activo"]);
                }
            }

            return cliente;
        }

        public bool Insertar(Cliente cliente)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarCliente", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@GeneroID", cliente.IdGenero);
                cmd.Parameters.AddWithValue("@NivelEducativoID", cliente.IdNivelEducativo);
                cmd.Parameters.AddWithValue("@RangoIngresosID", cliente.IdRangoIngresos);
                cmd.Parameters.AddWithValue("@RangoEdadID", cliente.IdRangoEdad);
                cmd.Parameters.AddWithValue("@OcupacionID", cliente.IdOcupacion);

                conn.Open();

                return cmd.ExecuteScalar() != null;
            }
        }

        public bool Actualizar(Cliente cliente)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarCliente", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClienteID", cliente.IdCliente);
                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@GeneroID", cliente.IdGenero);
                cmd.Parameters.AddWithValue("@NivelEducativoID", cliente.IdNivelEducativo);
                cmd.Parameters.AddWithValue("@RangoIngresosID", cliente.IdRangoIngresos);
                cmd.Parameters.AddWithValue("@RangoEdadID", cliente.IdRangoEdad);
                cmd.Parameters.AddWithValue("@OcupacionID", cliente.IdOcupacion);

                conn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarLogicoCliente", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClienteID", id);

                conn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
