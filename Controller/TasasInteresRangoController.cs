using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasasInteresRangoController : ControllerBase
    {
        private readonly TasaInteresRangoRepository _repo;

        public TasasInteresRangoController(
            TasaInteresRangoRepository repo)
        {
            _repo = repo;
        }


        // ============================================================
        // LISTAR RANGOS DE TASA
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
        // OBTENER RANGO DE TASA POR ID
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
                    "El ID del rango de tasa no es válido.");
            }

            var rango =
                _repo.ObtenerPorId(id);

            if (rango == null)
            {
                return NotFound(
                    "No se encontró el rango de tasa.");
            }

            return Ok(rango);
        }


        // ============================================================
        // CREAR RANGO DE TASA
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(
            TasaInteresRango item)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (item == null)
            {
                return BadRequest(
                    "Los datos del rango de tasa son obligatorios.");
            }

            if (_repo.Insertar(item))
            {
                return Ok(
                    "Rango de tasa agregado correctamente.");
            }

            return BadRequest(
                "No fue posible agregar el rango de tasa.");
        }


        // ============================================================
        // ACTUALIZAR RANGO DE TASA
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(
            TasaInteresRango item)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (item == null)
            {
                return BadRequest(
                    "Los datos del rango de tasa son obligatorios.");
            }

            if (_repo.Actualizar(item))
            {
                return Ok(
                    "Rango de tasa actualizado correctamente.");
            }

            return BadRequest(
                "No fue posible actualizar el rango de tasa.");
        }


        // ============================================================
        // ELIMINAR RANGO DE TASA
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
                    "El ID del rango de tasa no es válido.");
            }

            if (_repo.Eliminar(id))
            {
                return Ok(
                    "Rango de tasa eliminado correctamente.");
            }

            return BadRequest(
                "No fue posible eliminar el rango de tasa.");
        }
    }
}