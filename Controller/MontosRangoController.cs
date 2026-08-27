using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MontosRangoController : ControllerBase
    {
        private readonly MontoRangoRepository _repo;

        public MontosRangoController(
            MontoRangoRepository repo)
        {
            _repo = repo;
        }


        // ============================================================
        // LISTAR RANGOS DE MONTO
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
        // OBTENER RANGO DE MONTO POR ID
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
                    "El ID del rango de monto no es válido.");
            }

            var rango =
                _repo.ObtenerPorId(id);

            if (rango == null)
            {
                return NotFound(
                    "No se encontró el rango de monto.");
            }

            return Ok(rango);
        }


        // ============================================================
        // CREAR RANGO DE MONTO
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(
            MontoRango item)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (item == null)
            {
                return BadRequest(
                    "Los datos del rango de monto son obligatorios.");
            }

            if (_repo.Insertar(item))
            {
                return Ok(
                    "Rango de monto agregado correctamente.");
            }

            return BadRequest(
                "No fue posible agregar el rango de monto.");
        }


        // ============================================================
        // ACTUALIZAR RANGO DE MONTO
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(
            MontoRango item)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (item == null)
            {
                return BadRequest(
                    "Los datos del rango de monto son obligatorios.");
            }

            if (_repo.Actualizar(item))
            {
                return Ok(
                    "Rango de monto actualizado correctamente.");
            }

            return BadRequest(
                "No fue posible actualizar el rango de monto.");
        }


        // ============================================================
        // ELIMINAR RANGO DE MONTO
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
                    "El ID del rango de monto no es válido.");
            }

            if (_repo.Eliminar(id))
            {
                return Ok(
                    "Rango de monto eliminado correctamente.");
            }

            return BadRequest(
                "No fue posible eliminar el rango de monto.");
        }
    }
}