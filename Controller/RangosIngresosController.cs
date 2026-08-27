using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RangosIngresosController : ControllerBase
    {
        private readonly RangoIngresoRepository _repo;

        public RangosIngresosController(
            RangoIngresoRepository repo)
        {
            _repo = repo;
        }


        // ============================================================
        // LISTAR RANGOS DE INGRESOS
        // ADMINISTRADOR + ENCUESTADOR
        // ============================================================

        [HttpGet]
        public IActionResult Get(
            [FromQuery] bool incluirInactivos = false)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request) &&
                !AutorizacionApiHelper.EsEncuestador(Request))
            {
                return Forbid();
            }

            return Ok(_repo.Listar(incluirInactivos));
        }

        // ============================================================
        // OBTENER RANGO DE INGRESOS POR ID
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
                    "El ID del rango de ingresos no es válido.");
            }

            var rango =
                _repo.ObtenerPorId(id);

            if (rango == null)
            {
                return NotFound(
                    "No se encontró el rango de ingresos.");
            }

            return Ok(rango);
        }


        // ============================================================
        // CREAR RANGO DE INGRESOS
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(
            RangoIngreso rango)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (rango == null)
            {
                return BadRequest(
                    "Los datos del rango de ingresos son obligatorios.");
            }

            if (_repo.Insertar(rango))
            {
                return Ok(
                    "Rango de ingresos agregado correctamente.");
            }

            return BadRequest(
                "No fue posible agregar el rango de ingresos.");
        }


        // ============================================================
        // ACTUALIZAR RANGO DE INGRESOS
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(
            RangoIngreso rango)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (rango == null)
            {
                return BadRequest(
                    "Los datos del rango de ingresos son obligatorios.");
            }

            if (_repo.Actualizar(rango))
            {
                return Ok(
                    "Rango de ingresos actualizado correctamente.");
            }

            return BadRequest(
                "No fue posible actualizar el rango de ingresos.");
        }


        // ============================================================
        // ELIMINAR RANGO DE INGRESOS
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
                    "El ID del rango de ingresos no es válido.");
            }

            if (_repo.Eliminar(id))
            {
                return Ok(
                    "Rango de ingresos eliminado correctamente.");
            }

            return BadRequest(
                "No fue posible eliminar el rango de ingresos.");
        }

        // ============================================================
        // RESTAURAR RANGO DE INGRESOS
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
                    "El ID del rango de ingresos no es válido.");
            }

            return _repo.Restaurar(id)
                ? Ok("Rango de ingresos restaurado correctamente.")
                : BadRequest("No fue posible restaurar el rango de ingresos.");
        }
    }
}