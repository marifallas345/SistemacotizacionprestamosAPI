using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediosContratacionController : ControllerBase
    {
        private readonly MedioContratacionRepository _repo;

        public MediosContratacionController(
            MedioContratacionRepository repo)
        {
            _repo = repo;
        }


        // ============================================================
        // LISTAR MEDIOS DE CONTRATACIÓN
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
        // OBTENER MEDIO DE CONTRATACIÓN POR ID
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
                    "El ID del medio de contratación no es válido.");
            }

            var medio =
                _repo.ObtenerPorId(id);

            if (medio == null)
            {
                return NotFound(
                    "No se encontró el medio de contratación.");
            }

            return Ok(medio);
        }


        // ============================================================
        // CREAR MEDIO DE CONTRATACIÓN
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(
            MedioContratacion item)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (item == null)
            {
                return BadRequest(
                    "Los datos del medio de contratación son obligatorios.");
            }

            if (_repo.Insertar(item))
            {
                return Ok(
                    "Medio de contratación agregado correctamente.");
            }

            return BadRequest(
                "No fue posible agregar el medio de contratación.");
        }


        // ============================================================
        // ACTUALIZAR MEDIO DE CONTRATACIÓN
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(
            MedioContratacion item)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (item == null)
            {
                return BadRequest(
                    "Los datos del medio de contratación son obligatorios.");
            }

            if (_repo.Actualizar(item))
            {
                return Ok(
                    "Medio de contratación actualizado correctamente.");
            }

            return BadRequest(
                "No fue posible actualizar el medio de contratación.");
        }


        // ============================================================
        // ELIMINAR MEDIO DE CONTRATACIÓN
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
                    "El ID del medio de contratación no es válido.");
            }

            if (_repo.Eliminar(id))
            {
                return Ok(
                    "Medio de contratación eliminado correctamente.");
            }

            return BadRequest(
                "No fue posible eliminar el medio de contratación.");
        }
    }
}