using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class TipoPrestamoRepository
    {
        private readonly DbContext _context;

        public TipoPrestamoRepository(DbContext context)
        {
            _context = context;
        }

        // ============================================================
        // MAPEAR
        // ============================================================

        private TipoPrestamo Mapear(SqlDataReader dr)
        {
            return new TipoPrestamo
            {
                IdTipoPrestamo =
                    Convert.ToInt32(
                        dr["id_tipo_prestamo"]),

                Nombre =
                    dr["nombre"].ToString()!,

                Descripcion =
                    dr["descripcion"] as string,

                Activo =
                    Convert.ToBoolean(
                        dr["activo"])
            };
        }


        // ============================================================
        // LISTAR
        // ============================================================

        public List<TipoPrestamo> Listar(bool incluirInactivos = false)
        {
            List<TipoPrestamo> lista = new();

            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    "sp_ListarTiposPrestamo",
                    conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@IncluirInactivos",
                    incluirInactivos);

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(Mapear(dr));
                }
            }

            return lista;
        }

        // ============================================================
        // OBTENER POR ID
        // ============================================================

        public TipoPrestamo ObtenerPorId(int id)
        {
            TipoPrestamo item =
                new TipoPrestamo();

            using (SqlConnection conn =
                _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "sp_ObtenerTipoPrestamoPorId",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@TipoPrestamoID",
                    id);

                conn.Open();

                SqlDataReader dr =
                    cmd.ExecuteReader();

                if (dr.Read())
                {
                    item = Mapear(dr);
                }
            }

            return item;
        }


        // ============================================================
        // INSERTAR
        // ============================================================

        public bool Insertar(
            TipoPrestamo item)
        {
            using (SqlConnection conn =
                _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "sp_InsertarTipoPrestamo",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@Nombre",
                    item.Nombre);

                cmd.Parameters.AddWithValue(
                    "@Descripcion",
                    item.Descripcion ??
                    (object)DBNull.Value);

                conn.Open();

                return
                    cmd.ExecuteScalar() != null;
            }
        }


        // ============================================================
        // ACTUALIZAR
        // ============================================================

        public bool Actualizar(
            TipoPrestamo item)
        {
            using (SqlConnection conn =
                _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "sp_ActualizarTipoPrestamo",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@TipoPrestamoID",
                    item.IdTipoPrestamo);

                cmd.Parameters.AddWithValue(
                    "@Nombre",
                    item.Nombre);

                cmd.Parameters.AddWithValue(
                    "@Descripcion",
                    item.Descripcion ??
                    (object)DBNull.Value);

                conn.Open();

                return
                    cmd.ExecuteNonQuery() > 0;
            }
        }


        // ============================================================
        // ELIMINAR LÓGICO
        // ============================================================

        public bool Eliminar(int id)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    "sp_EliminarLogicoTipoPrestamo",
                    conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@TipoPrestamoID",
                    id);

                conn.Open();

                return cmd.ExecuteNonQuery() != 0;
            }
        }


        // ============================================================
        // RESTAURAR
        // ============================================================

        public bool Restaurar(int id)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    "sp_RestaurarTipoPrestamo",
                    conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@TipoPrestamoID",
                    id);

                conn.Open();

                return cmd.ExecuteNonQuery() != 0;
            }
        }
    }
}