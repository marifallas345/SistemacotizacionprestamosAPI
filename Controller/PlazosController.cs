using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlazosController : ControllerBase
    {
        private readonly PlazoRepository _repo;

        public PlazosController(PlazoRepository repo)
        {
            _repo = repo;
        }

        // ============================================================
        // LISTAR
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
        // OBTENER POR ID
        // ============================================================

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            var plazo = _repo.ObtenerPorId(id);

            if (plazo == null)
            {
                return NotFound(
                    "El plazo no existe o está inactivo.");
            }

            return Ok(plazo);
        }

        // ============================================================
        // INSERTAR
        // ============================================================

        [HttpPost]
        public IActionResult Post(Plazo plazo)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (plazo.Meses <= 0)
            {
                return BadRequest(
                    "La cantidad de meses debe ser mayor que cero.");
            }

            if (string.IsNullOrWhiteSpace(plazo.Descripcion))
            {
                return BadRequest(
                    "La descripción es obligatoria.");
            }

            return _repo.Insertar(plazo)
                ? Ok(
                    "Plazo agregado correctamente.")
                : BadRequest(
                    "No fue posible agregar el plazo.");
        }

        // ============================================================
        // ACTUALIZAR
        // ============================================================

        [HttpPut]
        public IActionResult Put(Plazo plazo)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (plazo.IdPlazo <= 0)
            {
                return BadRequest(
                    "El ID del plazo no es válido.");
            }

            if (plazo.Meses <= 0)
            {
                return BadRequest(
                    "La cantidad de meses debe ser mayor que cero.");
            }

            if (string.IsNullOrWhiteSpace(plazo.Descripcion))
            {
                return BadRequest(
                    "La descripción es obligatoria.");
            }

            return _repo.Actualizar(plazo)
                ? Ok(
                    "Plazo actualizado correctamente.")
                : BadRequest(
                    "No fue posible actualizar el plazo.");
        }

        // ============================================================
        // ELIMINAR LÓGICAMENTE
        // ============================================================

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            return _repo.Eliminar(id)
                ? Ok(
                    "Plazo eliminado lógicamente.")
                : BadRequest(
                    "No fue posible eliminar el plazo.");
        }

        // ============================================================
        // RESTAURAR
        // ============================================================

        [HttpPut("Restaurar/{id:int}")]
        public IActionResult Restaurar(int id)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            return _repo.Restaurar(id)
                ? Ok(
                    "Plazo restaurado correctamente.")
                : BadRequest(
                    "No fue posible restaurar el plazo.");
        }
    }
}