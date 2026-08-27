using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreguntasController : ControllerBase
    {
        private readonly PreguntaRepository _repo;

        public PreguntasController(
            PreguntaRepository repo)
        {
            _repo = repo;
        }


        // ============================================================
        // LISTAR PREGUNTAS
        // ADMINISTRADOR + ENCUESTADOR
        // ============================================================

        [HttpGet]
        public IActionResult Get(
            [FromQuery] bool incluirInactivos = false)
        {
            // --------------------------------------------------------
            // ADMINISTRADOR
            // Puede ver preguntas activas e inactivas
            // --------------------------------------------------------

            if (AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Ok(
                    _repo.Listar(incluirInactivos));
            }


            // --------------------------------------------------------
            // ENCUESTADOR
            // Solo puede consultar preguntas activas
            // --------------------------------------------------------

            if (AutorizacionApiHelper.EsEncuestador(Request))
            {
                // No puede solicitar preguntas eliminadas.
                if (incluirInactivos)
                {
                    return Forbid();
                }

                return Ok(
                    _repo.Listar(false));
            }


            // --------------------------------------------------------
            // CONSULTOR
            // No tiene acceso a preguntas
            // --------------------------------------------------------

            return Forbid();
        }


        // ============================================================
        // OBTENER PREGUNTA POR ID
        // ADMINISTRADOR + ENCUESTADOR
        // ============================================================

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request) &&
                !AutorizacionApiHelper.EsEncuestador(Request))
            {
                return Forbid();
            }

            if (id <= 0)
            {
                return BadRequest(
                    "El ID de la pregunta no es válido.");
            }

            var pregunta =
                _repo.ObtenerPorId(id);

            if (pregunta == null)
            {
                return NotFound(
                    "La pregunta no existe o está inactiva.");
            }

            return Ok(pregunta);
        }


        // ============================================================
        // CREAR PREGUNTA
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(
            Pregunta pregunta)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (pregunta == null)
            {
                return BadRequest(
                    "Los datos de la pregunta son obligatorios.");
            }

            if (string.IsNullOrWhiteSpace(
                pregunta.Texto))
            {
                return BadRequest(
                    "El texto de la pregunta es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(
                pregunta.TipoControl))
            {
                return BadRequest(
                    "El tipo de control es obligatorio.");
            }

            if (pregunta.IdCategoria <= 0)
            {
                return BadRequest(
                    "Debe seleccionar una categoría válida.");
            }

            if (_repo.Insertar(pregunta))
            {
                return Ok(
                    "Pregunta agregada correctamente.");
            }

            return BadRequest(
                "No fue posible agregar la pregunta.");
        }


        // ============================================================
        // ACTUALIZAR PREGUNTA
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(
            Pregunta pregunta)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (pregunta == null)
            {
                return BadRequest(
                    "Los datos de la pregunta son obligatorios.");
            }

            if (pregunta.IdPregunta <= 0)
            {
                return BadRequest(
                    "El ID de la pregunta no es válido.");
            }

            if (string.IsNullOrWhiteSpace(
                pregunta.Texto))
            {
                return BadRequest(
                    "El texto de la pregunta es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(
                pregunta.TipoControl))
            {
                return BadRequest(
                    "El tipo de control es obligatorio.");
            }

            if (pregunta.IdCategoria <= 0)
            {
                return BadRequest(
                    "Debe seleccionar una categoría válida.");
            }

            if (_repo.Actualizar(pregunta))
            {
                return Ok(
                    "Pregunta actualizada correctamente.");
            }

            return BadRequest(
                "No fue posible actualizar la pregunta.");
        }


        // ============================================================
        // ELIMINAR PREGUNTA
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (id <= 0)
            {
                return BadRequest(
                    "El ID de la pregunta no es válido.");
            }

            if (_repo.Eliminar(id))
            {
                return Ok(
                    "Pregunta eliminada lógicamente.");
            }

            return BadRequest(
                "No fue posible eliminar la pregunta.");
        }


        // ============================================================
        // RESTAURAR PREGUNTA
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
                    "El ID de la pregunta no es válido.");
            }

            if (_repo.Restaurar(id))
            {
                return Ok(
                    "Pregunta restaurada correctamente.");
            }

            return BadRequest(
                "No fue posible restaurar la pregunta.");
        }
    }
}