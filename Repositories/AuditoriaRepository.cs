using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class AuditoriaRepository
    {
        private readonly DbContext _context;

        public AuditoriaRepository(DbContext context)
        {
            _context = context;
        }

        public List<AuditoriaModel> Listar()
        {
            List<AuditoriaModel> lista = new List<AuditoriaModel>();

            using (SqlConnection conn = _context.CreateConnection())
            {
                string sql = @"
                    SELECT
                        id_auditoria,
                        id_usuario,
                        accion,
                        tabla_afectada,
                        id_registro_afectado,
                        detalle,
                        fecha_accion,
                        ip_origen
                    FROM Auditorias
                    ORDER BY fecha_accion DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AuditoriaModel auditoria = new AuditoriaModel
                        {
                            IdAuditoria = dr["id_auditoria"] == DBNull.Value
                                ? 0
                                : Convert.ToInt32(dr["id_auditoria"]),

                            IdUsuario = dr["id_usuario"] == DBNull.Value
                                ? 0
                                : Convert.ToInt32(dr["id_usuario"]),

                            Accion = dr["accion"] == DBNull.Value
                                ? ""
                                : dr["accion"].ToString(),

                            TablaAfectada = dr["tabla_afectada"] == DBNull.Value
                                ? ""
                                : dr["tabla_afectada"].ToString(),

                            IdRegistroAfectado = dr["id_registro_afectado"] == DBNull.Value
                                ? 0
                                : Convert.ToInt32(dr["id_registro_afectado"]),

                            Detalle = dr["detalle"] == DBNull.Value
                                ? ""
                                : dr["detalle"].ToString(),

                            FechaAccion = dr["fecha_accion"] == DBNull.Value
                                ? DateTime.MinValue
                                : Convert.ToDateTime(dr["fecha_accion"]),

                            IpOrigen = dr["ip_origen"] == DBNull.Value
                                ? ""
                                : dr["ip_origen"].ToString()
                        };

                        lista.Add(auditoria);
                    }
                }
            }

            return lista;
        }
    }
}