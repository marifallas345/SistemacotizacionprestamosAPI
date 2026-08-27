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


        // ============================================================
        // LISTAR OCUPACIONES
        // incluirInactivos = false -> solo activos
        // incluirInactivos = true  -> activos + inactivos
        // ============================================================

        public List<Ocupacion> Listar(
            bool incluirInactivos = false)
        {
            List<Ocupacion> lista = new List<Ocupacion>();

            using (SqlConnection conn =
                   _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "sp_ListarOcupaciones",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@IncluirInactivos",
                    incluirInactivos);

                conn.Open();

                using (SqlDataReader dr =
                       cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(
                            new Ocupacion
                            {
                                IdOcupacion =
                                    Convert.ToInt32(
                                        dr["id_ocupacion"]),

                                Nombre =
                                    dr["nombre"]
                                    .ToString()!,

                                Activo =
                                    Convert.ToBoolean(
                                        dr["activo"])
                            });
                    }
                }
            }

            return lista;
        }


        // ============================================================
        // OBTENER OCUPACIÓN POR ID
        // ============================================================

        public Ocupacion? ObtenerPorId(int id)
        {
            using (SqlConnection conn =
                   _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "sp_ObtenerOcupacionPorId",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@OcupacionID",
                    id);

                conn.Open();

                using (SqlDataReader dr =
                       cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return new Ocupacion
                        {
                            IdOcupacion =
                                Convert.ToInt32(
                                    dr["id_ocupacion"]),

                            Nombre =
                                dr["nombre"]
                                .ToString()!,

                            Activo =
                                Convert.ToBoolean(
                                    dr["activo"])
                        };
                    }
                }
            }

            return null;
        }


        // ============================================================
        // INSERTAR OCUPACIÓN
        // ============================================================

        public bool Insertar(
            Ocupacion ocupacion)
        {
            using (SqlConnection conn =
                   _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "sp_InsertarOcupacion",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@Nombre",
                    ocupacion.Nombre);

                conn.Open();

                return cmd.ExecuteScalar() != null;
            }
        }


        // ============================================================
        // ACTUALIZAR OCUPACIÓN
        // ============================================================

        public bool Actualizar(
            Ocupacion ocupacion)
        {
            using (SqlConnection conn =
                   _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "sp_ActualizarOcupacion",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@OcupacionID",
                    ocupacion.IdOcupacion);

                cmd.Parameters.AddWithValue(
                    "@Nombre",
                    ocupacion.Nombre);

                conn.Open();

                cmd.ExecuteNonQuery();

                return true;
            }
        }


        // ============================================================
        // ELIMINACIÓN LÓGICA
        // ============================================================

        public bool Eliminar(int id)
        {
            using (SqlConnection conn =
                   _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "sp_EliminarLogicoOcupacion",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@OcupacionID",
                    id);

                conn.Open();

                cmd.ExecuteNonQuery();

                return true;
            }
        }


        // ============================================================
        // RESTAURAR OCUPACIÓN
        // ============================================================

        public bool Restaurar(int id)
        {
            using (SqlConnection conn =
                   _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "sp_RestaurarOcupacion",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@OcupacionID",
                    id);

                // El procedimiento permite NULL
                // para UsuarioModificacionID.
                cmd.Parameters.AddWithValue(
                    "@UsuarioModificacionID",
                    DBNull.Value);

                conn.Open();

                cmd.ExecuteNonQuery();

                return true;
            }
        }
    }
}