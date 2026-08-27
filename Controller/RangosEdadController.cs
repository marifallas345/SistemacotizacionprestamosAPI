using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RangosEdadController : ControllerBase
    {
        private readonly RangoEdadRepository _repo;

        public RangosEdadController(
            RangoEdadRepository repo)
        {
            _repo = repo;
        }


        // ============================================================
        // LISTAR RANGOS DE EDAD
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
        // OBTENER RANGO DE EDAD POR ID
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
                    "El ID del rango de edad no es válido.");
            }

            var rango =
                _repo.ObtenerPorId(id);

            if (rango == null)
            {
                return NotFound(
                    "No se encontró el rango de edad.");
            }

            return Ok(rango);
        }


        // ============================================================
        // CREAR RANGO DE EDAD
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(
            RangoEdad item)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (item == null)
            {
                return BadRequest(
                    "Los datos del rango de edad son obligatorios.");
            }

            if (_repo.Insertar(item))
            {
                return Ok(
                    "Rango de edad agregado correctamente.");
            }

            return BadRequest(
                "No fue posible agregar el rango de edad.");
        }


        // ============================================================
        // ACTUALIZAR RANGO DE EDAD
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(
            RangoEdad item)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (item == null)
            {
                return BadRequest(
                    "Los datos del rango de edad son obligatorios.");
            }

            if (_repo.Actualizar(item))
            {
                return Ok(
                    "Rango de edad actualizado correctamente.");
            }

            return BadRequest(
                "No fue posible actualizar el rango de edad.");
        }


        // ============================================================
        // ELIMINAR RANGO DE EDAD
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
                    "El ID del rango de edad no es válido.");
            }

            if (_repo.Eliminar(id))
            {
                return Ok(
                    "Rango de edad eliminado correctamente.");
            }

            return BadRequest(
                "No fue posible eliminar el rango de edad.");
        }
    }
}