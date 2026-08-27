using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NivelesEducativosController : ControllerBase
    {
        private readonly NivelEducativoRepository _repo;

        public NivelesEducativosController(
            NivelEducativoRepository repo)
        {
            _repo = repo;
        }


        // ============================================================
        // LISTAR NIVELES EDUCATIVOS
        // ADMINISTRADOR + ENCUESTADOR
        // ============================================================

        [HttpGet]
        public IActionResult Get()
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request) &&
                !AutorizacionApiHelper.EsEncuestador(Request))
            {
                return Forbid();
            }

            return Ok(_repo.Listar());
        }


        // ============================================================
        // OBTENER NIVEL EDUCATIVO POR ID
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
                    "El ID del nivel educativo no es válido.");
            }

            var nivel =
                _repo.ObtenerPorId(id);

            if (nivel == null)
            {
                return NotFound(
                    "No se encontró el nivel educativo.");
            }

            return Ok(nivel);
        }


        // ============================================================
        // CREAR NIVEL EDUCATIVO
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(
            NivelEducativo nivel)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (nivel == null)
            {
                return BadRequest(
                    "Los datos del nivel educativo son obligatorios.");
            }

            if (_repo.Insertar(nivel))
            {
                return Ok(
                    "Nivel educativo agregado correctamente.");
            }

            return BadRequest(
                "No fue posible agregar el nivel educativo.");
        }


        // ============================================================
        // ACTUALIZAR NIVEL EDUCATIVO
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(
            NivelEducativo nivel)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (nivel == null)
            {
                return BadRequest(
                    "Los datos del nivel educativo son obligatorios.");
            }

            if (_repo.Actualizar(nivel))
            {
                return Ok(
                    "Nivel educativo actualizado correctamente.");
            }

            return BadRequest(
                "No fue posible actualizar el nivel educativo.");
        }


        // ============================================================
        // ELIMINAR NIVEL EDUCATIVO
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
                    "El ID del nivel educativo no es válido.");
            }

            if (_repo.Eliminar(id))
            {
                return Ok(
                    "Nivel educativo eliminado correctamente.");
            }

            return BadRequest(
                "No fue posible eliminar el nivel educativo.");
        }
    }
}