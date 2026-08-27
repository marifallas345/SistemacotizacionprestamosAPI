using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespuestasController : ControllerBase
    {
        private readonly RespuestaRepository _repo;

        public RespuestasController(
            RespuestaRepository repo)
        {
            _repo = repo;
        }


        // ============================================================
        // LISTAR RESPUESTAS
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpGet]
        public IActionResult Get(
            [FromQuery] bool incluirInactivos = false)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            return Ok(
                _repo.Listar(incluirInactivos));
        }


        // ============================================================
        // OBTENER RESPUESTA POR ID
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (id <= 0)
            {
                return BadRequest(
                    "El ID de la respuesta no es válido.");
            }

            var respuesta =
                _repo.ObtenerPorId(id);

            if (respuesta == null)
            {
                return NotFound(
                    "La respuesta no existe o está inactiva.");
            }

            return Ok(respuesta);
        }


        // ============================================================
        // INSERTAR RESPUESTA
        // ADMINISTRADOR + ENCUESTADOR
        //
        // El Encuestador necesita este endpoint para registrar
        // las respuestas de una nueva encuesta.
        // ============================================================

        [HttpPost]
        public IActionResult Post(
            Respuesta respuesta)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request) &&
                !AutorizacionApiHelper.EsEncuestador(Request))
            {
                return Forbid();
            }

            if (respuesta == null)
            {
                return BadRequest(
                    "Los datos de la respuesta son obligatorios.");
            }

            if (respuesta.IdEncuesta <= 0)
            {
                return BadRequest(
                    "Debe indicar una encuesta válida.");
            }

            if (respuesta.IdPregunta <= 0)
            {
                return BadRequest(
                    "Debe indicar una pregunta válida.");
            }

            if (_repo.Insertar(respuesta))
            {
                return Ok(
                    "Respuesta agregada correctamente.");
            }

            return BadRequest(
                "No fue posible agregar la respuesta.");
        }


        // ============================================================
        // ACTUALIZAR RESPUESTA
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(
            Respuesta respuesta)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (respuesta == null)
            {
                return BadRequest(
                    "Los datos de la respuesta son obligatorios.");
            }

            if (respuesta.IdRespuesta <= 0)
            {
                return BadRequest(
                    "El ID de la respuesta no es válido.");
            }

            if (respuesta.IdEncuesta <= 0)
            {
                return BadRequest(
                    "Debe indicar una encuesta válida.");
            }

            if (respuesta.IdPregunta <= 0)
            {
                return BadRequest(
                    "Debe indicar una pregunta válida.");
            }

            if (_repo.Actualizar(respuesta))
            {
                return Ok(
                    "Respuesta actualizada correctamente.");
            }

            return BadRequest(
                "No fue posible actualizar la respuesta.");
        }


        // ============================================================
        // ELIMINAR RESPUESTA
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
                    "El ID de la respuesta no es válido.");
            }

            if (_repo.Eliminar(id))
            {
                return Ok(
                    "Respuesta eliminada lógicamente.");
            }

            return BadRequest(
                "No fue posible eliminar la respuesta.");
        }


        // ============================================================
        // RESTAURAR RESPUESTA
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
                    "El ID de la respuesta no es válido.");
            }

            if (_repo.Restaurar(id))
            {
                return Ok(
                    "Respuesta restaurada correctamente.");
            }

            return BadRequest(
                "No fue posible restaurar la respuesta.");
        }
    }
}