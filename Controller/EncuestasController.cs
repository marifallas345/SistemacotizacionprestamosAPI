using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EncuestasController : ControllerBase
    {
        private readonly EncuestaRepository _repo;

        public EncuestasController(EncuestaRepository repo)
        {
            _repo = repo;
        }

        // ============================================================
        // GUARDAR ENCUESTA COMPLETA
        // ADMINISTRADOR Y ENCUESTADOR
        // ============================================================

        [HttpPost("GuardarCompleta")]
        public IActionResult GuardarCompleta(
            EncuestaCompletaModel encuesta)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request) &&
                !AutorizacionApiHelper.EsEncuestador(Request))
            {
                return Forbid();
            }

            try
            {
                // El usuario que viene del header es la fuente
                // de identificación para la encuesta.
                int idUsuario =
                    AutorizacionApiHelper.ObtenerIdUsuario(Request);

                if (idUsuario <= 0)
                {
                    return Unauthorized(
                        "No se pudo identificar al usuario.");
                }

                // No permitimos que el cliente Web envíe
                // otro usuario diferente al usuario autenticado.
                encuesta.IdUsuario = idUsuario;

                if (_repo.GuardarEncuestaCompleta(encuesta))
                    return Ok(
                        "Encuesta almacenada correctamente.");

                return BadRequest(
                    "No fue posible almacenar la encuesta.");
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    "Error al guardar la encuesta: "
                    + ex.Message);
            }
        }


        // ============================================================
        // CONTADOR DE ENCUESTAS ACTIVAS
        // ADMINISTRADOR Y CONSULTOR
        // ============================================================

        [HttpGet("Contador")]
        public IActionResult Contador()
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request) &&
                !AutorizacionApiHelper.EsConsultor(Request))
            {
                return Forbid();
            }

            try
            {
                return Ok(
                    _repo.ContarEncuestasActivas());
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    "Error al contar encuestas: "
                    + ex.Message);
            }
        }


        // ============================================================
        // LISTAR ENCUESTAS
        //
        // ADMINISTRADOR → todas
        // ENCUESTADOR   → solamente las propias
        // CONSULTOR     → NO
        // ============================================================

        [HttpGet("Listar")]
        public IActionResult Listar(
            bool incluirEliminados = false)
        {
            if (AutorizacionApiHelper.EsAdministrador(Request))
            {
                try
                {
                    return Ok(
                        _repo.ListarEncuestas(
                            incluirEliminados));
                }
                catch (Exception ex)
                {
                    return StatusCode(
                        500,
                        "Error al listar encuestas: "
                        + ex.Message);
                }
            }

            if (AutorizacionApiHelper.EsEncuestador(Request))
            {
                try
                {
                    int idUsuario =
                        AutorizacionApiHelper.ObtenerIdUsuario(
                            Request);

                    if (idUsuario <= 0)
                    {
                        return Unauthorized(
                            "No se pudo identificar al usuario.");
                    }

                    // El encuestador NO puede decidir qué
                    // id_usuario consultar.
                    return Ok(
                        _repo.ListarEncuestas(
                            false,
                            idUsuario));
                }
                catch (Exception ex)
                {
                    return StatusCode(
                        500,
                        "Error al listar las encuestas: "
                        + ex.Message);
                }
            }

            return Forbid();
        }


        // ============================================================
        // OBTENER DETALLE
        //
        // ADMINISTRADOR → cualquier encuesta
        // ENCUESTADOR   → solamente una propia
        // CONSULTOR     → NO
        // ============================================================

        [HttpGet("Detalle/{id:int}")]
        public IActionResult ObtenerDetalle(int id)
        {
            if (id <= 0)
            {
                return BadRequest(
                    "El ID de la encuesta no es válido.");
            }

            if (!AutorizacionApiHelper.EsAdministrador(Request) &&
                !AutorizacionApiHelper.EsEncuestador(Request))
            {
                return Forbid();
            }

            try
            {
                DetalleEncuesta? encuesta =
                    _repo.ObtenerDetalleEncuesta(id);

                if (encuesta == null)
                {
                    return NotFound(
                        "No se encontró la encuesta.");
                }

                // Si es encuestador, solamente puede consultar
                // encuestas creadas por él mismo.
                if (AutorizacionApiHelper.EsEncuestador(Request))
                {
                    int idUsuario =
                        AutorizacionApiHelper.ObtenerIdUsuario(
                            Request);

                    if (idUsuario <= 0)
                    {
                        return Unauthorized(
                            "No se pudo identificar al usuario.");
                    }

                    if (!encuesta.IdUsuario.HasValue ||
                        encuesta.IdUsuario.Value != idUsuario)
                    {
                        return Forbid();
                    }
                }

                return Ok(encuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    "Error al obtener el detalle de la encuesta: "
                    + ex.Message);
            }
        }


        // ============================================================
        // ELIMINAR ENCUESTA
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpDelete("Eliminar/{id:int}")]
        public IActionResult Eliminar(int id)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (id <= 0)
            {
                return BadRequest(
                    "El ID de la encuesta no es válido.");
            }

            try
            {
                if (_repo.EliminarEncuesta(id))
                {
                    return Ok(
                        "Encuesta eliminada correctamente.");
                }

                return NotFound(
                    "No se encontró la encuesta.");
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    "Error al eliminar encuesta: "
                    + ex.Message);
            }
        }


        // ============================================================
        // RESTAURAR ENCUESTA
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut("Restaurar/{id:int}")]
        public IActionResult Restaurar(int id)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (id <= 0)
            {
                return BadRequest(
                    "El ID de la encuesta no es válido.");
            }

            try
            {
                if (_repo.RestaurarEncuesta(id))
                {
                    return Ok(
                        "Encuesta restaurada correctamente.");
                }

                return NotFound(
                    "No se encontró la encuesta.");
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    "Error al restaurar encuesta: "
                    + ex.Message);
            }
        }


        // ============================================================
        // BUSCAR POR NOMBRE
        //
        // ADMINISTRADOR → puede buscar
        // ENCUESTADOR   → puede buscar, pero después
        //                 verificamos que sea su encuesta
        // CONSULTOR     → NO
        // ============================================================

        [HttpGet("BuscarPorNombre/{nombre}")]
        public IActionResult BuscarPorNombre(
            string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    return BadRequest(
                        "Debe indicar un nombre.");
                }


                // ============================================================
                // ADMINISTRADOR
                // Puede buscar cualquier encuesta
                // ============================================================

                if (AutorizacionApiHelper.EsAdministrador(Request))
                {
                    var encuestaAdministrador =
                        _repo.BuscarEncuestaPorNombre(
                            nombre,
                            null);

                    if (encuestaAdministrador == null)
                    {
                        return NotFound(
                            "No se encontró la encuesta.");
                    }

                    return Ok(encuestaAdministrador);
                }


                // ============================================================
                // ENCUESTADOR
                // Solamente puede buscar sus propias encuestas
                // ============================================================

                if (AutorizacionApiHelper.EsEncuestador(Request))
                {
                    int idUsuario =
                        AutorizacionApiHelper.ObtenerIdUsuario(
                            Request);

                    if (idUsuario <= 0)
                    {
                        return Unauthorized(
                            "No se pudo identificar al usuario.");
                    }

                    var encuestaEncuestador =
                        _repo.BuscarEncuestaPorNombre(
                            nombre,
                            idUsuario);

                    if (encuestaEncuestador == null)
                    {
                        return NotFound(
                            "No se encontró una encuesta con ese nombre "
                            + "perteneciente al usuario actual.");
                    }

                    return Ok(encuestaEncuestador);
                }


                // ============================================================
                // CONSULTOR
                // No puede consultar encuestas
                // ============================================================

                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    "Error al buscar encuesta: "
                    + ex.Message);
            }
        }
    }
}