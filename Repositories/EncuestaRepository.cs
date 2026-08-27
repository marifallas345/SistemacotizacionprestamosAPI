using Microsoft.Data.SqlClient;
using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Models;
using System.Data;
using System.Text;

namespace SistemacotizacionprestamosAPI.Repositories
{
    public class EncuestaRepository
    {
        private readonly DbContext _context;

        public EncuestaRepository(DbContext context)
        {
            _context = context;
        }

        // ============================================================
        // GUARDAR ENCUESTA COMPLETA
        // ============================================================

        public bool GuardarEncuestaCompleta(EncuestaCompletaModel encuesta)
        {
            using (SqlConnection conn = _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    "sp_GuardarEncuestaCompleta",
                    conn);

                cmd.CommandType = CommandType.StoredProcedure;

                StringBuilder xml = new StringBuilder();

                xml.Append("<Respuestas>");

                foreach (var r in encuesta.Respuestas)
                {
                    xml.Append("<Respuesta>");

                    xml.Append(
                        $"<IdPregunta>{r.IdPregunta}</IdPregunta>");

                    xml.Append(
                        $"<ValorTexto>{r.ValorTexto ?? ""}</ValorTexto>");

                    xml.Append("</Respuesta>");
                }

                xml.Append("</Respuestas>");

                cmd.Parameters.AddWithValue(
                    "@Nombre",
                    encuesta.Nombre);

                cmd.Parameters.AddWithValue(
                    "@Apellidos",
                    encuesta.Apellidos);

                cmd.Parameters.AddWithValue(
                    "@Email",
                    encuesta.Email);

                cmd.Parameters.AddWithValue(
                    "@Telefono",
                    encuesta.Telefono);

                cmd.Parameters.AddWithValue(
                    "@IdGenero",
                    encuesta.IdGenero);

                cmd.Parameters.AddWithValue(
                    "@IdNivelEducativo",
                    encuesta.IdNivelEducativo);

                cmd.Parameters.AddWithValue(
                    "@IdRangoIngresos",
                    encuesta.IdRangoIngresos);

                cmd.Parameters.AddWithValue(
                    "@IdRangoEdad",
                    encuesta.IdRangoEdad);

                cmd.Parameters.AddWithValue(
                    "@IdOcupacion",
                    encuesta.IdOcupacion);

                cmd.Parameters.AddWithValue(
                    "@IdUsuario",
                    encuesta.IdUsuario ??
                    (object)DBNull.Value);

                cmd.Parameters.AddWithValue(
                    "@IpOrigen",
                    encuesta.IpOrigen ?? "");

                SqlParameter parametroXml =
                    new SqlParameter(
                        "@RespuestasXml",
                        SqlDbType.Xml);

                parametroXml.Value = xml.ToString();

                cmd.Parameters.Add(parametroXml);

                conn.Open();

                using (SqlDataReader dr =
                       cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        int exito =
                            Convert.ToInt32(dr["Exito"]);

                        if (exito == 1)
                            return true;

                        string mensaje =
                            dr["Mensaje"].ToString()
                            ?? "Error desconocido.";

                        throw new Exception(
                            "SQL dijo: " + mensaje);
                    }
                }

                return false;
            }
        }


        // ============================================================
        // CONTAR ENCUESTAS ACTIVAS
        // ============================================================

        public int ContarEncuestasActivas()
        {
            using (SqlConnection conn =
                   _context.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Encuestas WHERE activo = 1",
                    conn);

                conn.Open();

                return Convert.ToInt32(
                    cmd.ExecuteScalar());
            }
        }


        // ============================================================
        // LISTAR ENCUESTAS
        // ============================================================

        public List<EncuestaListadoModel> ListarEncuestas(
            bool incluirEliminados = false,
            int? idUsuario = null)
        {
            List<EncuestaListadoModel> lista =
                new List<EncuestaListadoModel>();

            using (SqlConnection conn = _context.CreateConnection())
            {
                string sql = @"
            SELECT
                e.id_encuesta,
                c.nombre + ' ' + c.apellidos AS NombreCompleto,
                c.email,
                c.telefono,
                e.fecha_registro,
                e.activo
            FROM Encuestas e
            INNER JOIN Clientes c
                ON e.id_cliente = c.id_cliente
            WHERE 1 = 1 ";

                // Si se indicó un usuario, solamente obtiene
                // las encuestas creadas por ese usuario.
                if (idUsuario.HasValue)
                {
                    sql += " AND e.id_usuario = @IdUsuario ";
                }

                // Si no se solicita incluir eliminadas,
                // solamente muestra las activas.
                if (!incluirEliminados)
                {
                    sql += " AND e.activo = 1 ";
                }

                sql += " ORDER BY e.fecha_registro DESC ";

                SqlCommand cmd = new SqlCommand(sql, conn);

                if (idUsuario.HasValue)
                {
                    cmd.Parameters.AddWithValue(
                        "@IdUsuario",
                        idUsuario.Value);
                }

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        EncuestaListadoModel encuesta =
                            new EncuestaListadoModel
                            {
                                IdEncuesta =
                                    Convert.ToInt32(
                                        dr["id_encuesta"]),

                                NombreCompleto =
                                    dr["NombreCompleto"]
                                    .ToString(),

                                Email =
                                    dr["email"]
                                    .ToString(),

                                Telefono =
                                    dr["telefono"]
                                    .ToString(),

                                FechaCreacion =
                                    Convert.ToDateTime(
                                        dr["fecha_registro"]),

                                Activo =
                                    Convert.ToBoolean(
                                        dr["activo"])
                            };

                        lista.Add(encuesta);
                    }
                }
            }

            return lista;
        }


        // ============================================================
        // ELIMINAR ENCUESTA
        // ============================================================

        public bool EliminarEncuesta(int idEncuesta)
        {
            using (SqlConnection conn =
                   _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(@"
                        UPDATE Encuestas
                        SET activo = 0,
                            fecha_modificacion = GETDATE()
                        WHERE id_encuesta = @IdEncuesta",
                        conn);

                cmd.Parameters.AddWithValue(
                    "@IdEncuesta",
                    idEncuesta);

                conn.Open();

                int filas =
                    cmd.ExecuteNonQuery();

                return filas > 0;
            }
        }


        // ============================================================
        // RESTAURAR ENCUESTA
        // ============================================================

        public bool RestaurarEncuesta(int idEncuesta)
        {
            using (SqlConnection conn =
                   _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(@"
                        UPDATE Encuestas
                        SET activo = 1,
                            fecha_modificacion = GETDATE()
                        WHERE id_encuesta = @IdEncuesta",
                        conn);

                cmd.Parameters.AddWithValue(
                    "@IdEncuesta",
                    idEncuesta);

                conn.Open();

                int filas =
                    cmd.ExecuteNonQuery();

                return filas > 0;
            }
        }


        // ============================================================
        // BUSCAR ENCUESTA POR NOMBRE
        // ============================================================

        public object BuscarEncuestaPorNombre(
            string nombre,
            int? idUsuario = null)
        {
            using (SqlConnection conn =
                   _context.CreateConnection())
            {
                string sql = @"
            SELECT TOP 1
                e.id_encuesta,
                e.id_usuario,
                c.nombre + ' ' + c.apellidos AS Cliente,
                e.activo
            FROM Encuestas e
            INNER JOIN Clientes c
                ON e.id_cliente = c.id_cliente
            WHERE c.nombre + ' ' + c.apellidos
                LIKE '%' + @Nombre + '%'
        ";

                // Si se indicó un usuario,
                // solamente busca sus propias encuestas.
                if (idUsuario.HasValue)
                {
                    sql += @"
                AND e.id_usuario = @IdUsuario
            ";
                }

                sql += @"
            ORDER BY e.fecha_registro DESC
        ";

                SqlCommand cmd =
                    new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue(
                    "@Nombre",
                    nombre);

                if (idUsuario.HasValue)
                {
                    cmd.Parameters.AddWithValue(
                        "@IdUsuario",
                        idUsuario.Value);
                }

                conn.Open();

                using (SqlDataReader dr =
                       cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return new
                        {
                            IdEncuesta =
                                Convert.ToInt32(
                                    dr["id_encuesta"]),

                            IdUsuario =
                                dr["id_usuario"] == DBNull.Value
                                    ? (int?)null
                                    : Convert.ToInt32(
                                        dr["id_usuario"]),

                            Cliente =
                                dr["Cliente"].ToString(),

                            Activo =
                                Convert.ToBoolean(
                                    dr["activo"])
                        };
                    }
                }

                return null;
            }
        }

        // ============================================================
        // DETALLE COMPLETO DE UNA ENCUESTA
        // ============================================================

        public DetalleEncuesta? ObtenerDetalleEncuesta(
            int idEncuesta)
        {
            DetalleEncuesta? encuesta = null;

            using (SqlConnection conn =
                   _context.CreateConnection())
            {
                string sql = @"
                    SELECT
                        e.id_encuesta,
                        e.id_cliente,

                        c.nombre,
                        c.apellidos,
                        c.email,
                        c.telefono,

                        e.id_usuario,
                        e.fecha_registro,
                        e.ip_origen,
                        e.activo,

                        p.id_pregunta,
                        p.texto,
                        p.tipo_control,
                        p.orden,

                        r.valor_texto,
                        r.valor_entero,
                        r.valor_decimal

                    FROM Encuestas e

                    INNER JOIN Clientes c
                        ON e.id_cliente = c.id_cliente

                    LEFT JOIN Respuestas r
                        ON e.id_encuesta = r.id_encuesta

                    LEFT JOIN Preguntas p
                        ON r.id_pregunta = p.id_pregunta

                    WHERE e.id_encuesta = @IdEncuesta

                    ORDER BY p.orden;
                ";

                SqlCommand cmd =
                    new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue(
                    "@IdEncuesta",
                    idEncuesta);

                conn.Open();

                using (SqlDataReader dr =
                       cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        // --------------------------------------------
                        // INFORMACIÓN GENERAL
                        // --------------------------------------------

                        if (encuesta == null)
                        {
                            encuesta =
                                new DetalleEncuesta();

                            encuesta.IdEncuesta =
                                Convert.ToInt32(
                                    dr["id_encuesta"]);

                            encuesta.IdCliente =
                                Convert.ToInt32(
                                    dr["id_cliente"]);

                            encuesta.NombreCliente =
                                (dr["nombre"]
                                    .ToString() ?? "")
                                + " " +
                                (dr["apellidos"]
                                    .ToString() ?? "");

                            encuesta.Email =
                                dr["email"]
                                .ToString() ?? "";

                            encuesta.Telefono =
                                dr["telefono"]
                                .ToString() ?? "";

                            encuesta.IdUsuario =
                                dr["id_usuario"] ==
                                DBNull.Value
                                    ? null
                                    : Convert.ToInt32(
                                        dr["id_usuario"]);

                            encuesta.FechaRegistro =
                                Convert.ToDateTime(
                                    dr["fecha_registro"]);

                            encuesta.IpOrigen =
                                dr["ip_origen"] ==
                                DBNull.Value
                                    ? ""
                                    : dr["ip_origen"]
                                        .ToString() ?? "";

                            encuesta.Activo =
                                Convert.ToBoolean(
                                    dr["activo"]);
                        }


                        // --------------------------------------------
                        // PREGUNTA Y RESPUESTA
                        // --------------------------------------------

                        if (dr["id_pregunta"] !=
                            DBNull.Value)
                        {
                            DetalleRespuestaEncuesta respuesta =
                                new DetalleRespuestaEncuesta();

                            respuesta.IdPregunta =
                                Convert.ToInt32(
                                    dr["id_pregunta"]);

                            respuesta.Pregunta =
                                dr["texto"]
                                .ToString() ?? "";

                            respuesta.TipoControl =
                                dr["tipo_control"]
                                .ToString() ?? "";

                            respuesta.Orden =
                                Convert.ToInt32(
                                    dr["orden"]);

                            respuesta.ValorTexto =
                                dr["valor_texto"] ==
                                DBNull.Value
                                    ? ""
                                    : dr["valor_texto"]
                                        .ToString() ?? "";

                            respuesta.ValorEntero =
                                dr["valor_entero"] ==
                                DBNull.Value
                                    ? null
                                    : Convert.ToInt32(
                                        dr["valor_entero"]);

                            respuesta.ValorDecimal =
                                dr["valor_decimal"] ==
                                DBNull.Value
                                    ? null
                                    : Convert.ToDecimal(
                                        dr["valor_decimal"]);

                            encuesta.Respuestas.Add(
                                respuesta);
                        }
                    }
                }
            }

            return encuesta;
        }


        // ============================================================
        // AUDITORÍA
        // ============================================================

        public void RegistrarAuditoria(
            int idUsuario,
            string accion,
            string tabla,
            int idRegistro,
            string detalle,
            string ipOrigen)
        {
            using (SqlConnection conn =
                   _context.CreateConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(@"
                        INSERT INTO Auditorias
                        (
                            id_usuario,
                            accion,
                            tabla_afectada,
                            id_registro_afectado,
                            detalle,
                            fecha_accion,
                            ip_origen
                        )
                        VALUES
                        (
                            @IdUsuario,
                            @Accion,
                            @TablaAfectada,
                            @IdRegistroAfectado,
                            @Detalle,
                            GETDATE(),
                            @IpOrigen
                        )",
                        conn);

                cmd.Parameters.AddWithValue(
                    "@IdUsuario",
                    idUsuario);

                cmd.Parameters.AddWithValue(
                    "@Accion",
                    accion);

                cmd.Parameters.AddWithValue(
                    "@TablaAfectada",
                    tabla);

                cmd.Parameters.AddWithValue(
                    "@IdRegistroAfectado",
                    idRegistro);

                cmd.Parameters.AddWithValue(
                    "@Detalle",
                    detalle);

                cmd.Parameters.AddWithValue(
                    "@IpOrigen",
                    ipOrigen ?? "");

                conn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}