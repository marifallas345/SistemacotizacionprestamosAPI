using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class HistorialCrediticioRepository
    {
        private readonly DbContext _context;

        public HistorialCrediticioRepository(DbContext context)
        {
            _context = context;
        }

        public List<HistorialCrediticio> Listar(bool incluirInactivos = false)
        {
            List<HistorialCrediticio> lista = new();

            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ListarHistorialesCrediticios",
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

        public HistorialCrediticio? ObtenerPorId(int id)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ObtenerHistorialCrediticioPorId",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@HistorialID",
                id);

            conn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return Mapear(dr);
            }

            return null;
        }

        public bool Insertar(HistorialCrediticio historial)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_InsertarHistorialCrediticio",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@TienePrestamosPrevios",
                historial.TienePrestamosPrevios);

            cmd.Parameters.AddWithValue(
                "@HaMorado",
                historial.HaMorado);

            cmd.Parameters.AddWithValue(
                "@Descripcion",
                (object?)historial.Descripcion ?? DBNull.Value);

            cmd.Parameters.AddWithValue(
                "@UsuarioCreacionID",
                historial.UsuarioCreacion.HasValue
                    ? historial.UsuarioCreacion.Value
                    : DBNull.Value);

            conn.Open();

            return cmd.ExecuteScalar() != null;
        }

        public bool Actualizar(HistorialCrediticio historial)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_ActualizarHistorialCrediticio",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@HistorialID",
                historial.IdHistorial);

            cmd.Parameters.AddWithValue(
                "@TienePrestamosPrevios",
                historial.TienePrestamosPrevios);

            cmd.Parameters.AddWithValue(
                "@HaMorado",
                historial.HaMorado);

            cmd.Parameters.AddWithValue(
                "@Descripcion",
                (object?)historial.Descripcion ?? DBNull.Value);

            cmd.Parameters.AddWithValue(
                "@UsuarioModificacionID",
                historial.UsuarioModificacion.HasValue
                    ? historial.UsuarioModificacion.Value
                    : DBNull.Value);

            conn.Open();

            cmd.ExecuteNonQuery();

            return true;
        }

        public bool Eliminar(int id, int? usuarioModificacion = null)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_EliminarLogicoHistorialCrediticio",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@HistorialID",
                id);

            cmd.Parameters.AddWithValue(
                "@UsuarioModificacionID",
                usuarioModificacion.HasValue
                    ? usuarioModificacion.Value
                    : DBNull.Value);

            conn.Open();

            cmd.ExecuteNonQuery();

            return true;
        }

        public bool Restaurar(int id, int? usuarioModificacion = null)
        {
            using SqlConnection conn = _context.CreateConnection();

            using SqlCommand cmd = new SqlCommand(
                "sp_RestaurarHistorialCrediticio",
                conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@HistorialID",
                id);

            cmd.Parameters.AddWithValue(
                "@UsuarioModificacionID",
                usuarioModificacion.HasValue
                    ? usuarioModificacion.Value
                    : DBNull.Value);

            conn.Open();

            cmd.ExecuteNonQuery();

            return true;
        }

        private static HistorialCrediticio Mapear(SqlDataReader dr)
        {
            return new HistorialCrediticio
            {
                IdHistorial = Convert.ToInt32(
                    dr["id_historial"]),

                TienePrestamosPrevios = Convert.ToBoolean(
                    dr["tiene_prestamos_previos"]),

                HaMorado = Convert.ToBoolean(
                    dr["ha_morado"]),

                Descripcion = dr["descripcion"] == DBNull.Value
                    ? null
                    : dr["descripcion"].ToString(),

                Activo = Convert.ToBoolean(
                    dr["activo"]),

                FechaCreacion = Convert.ToDateTime(
                    dr["fecha_creacion"]),

                UsuarioCreacion = dr["usuario_creacion"] == DBNull.Value
                    ? null
                    : Convert.ToInt32(dr["usuario_creacion"]),

                FechaModificacion = dr["fecha_modificacion"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(dr["fecha_modificacion"]),

                UsuarioModificacion = dr["usuario_modificacion"] == DBNull.Value
                    ? null
                    : Convert.ToInt32(dr["usuario_modificacion"])
            };
        }
    }
}