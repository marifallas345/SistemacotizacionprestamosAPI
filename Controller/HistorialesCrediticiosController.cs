using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialesCrediticiosController : ControllerBase
    {
        private readonly HistorialCrediticioRepository _repo;

        public HistorialesCrediticiosController(
            HistorialCrediticioRepository repo)
        {
            _repo = repo;
        }


        // ============================================================
        // LISTAR HISTORIALES CREDITICIOS
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
        // OBTENER HISTORIAL CREDITICIO POR ID
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
                    "El ID del historial no es válido.");
            }

            var historial =
                _repo.ObtenerPorId(id);

            if (historial == null)
            {
                return NotFound(
                    "El historial crediticio no existe o está inactivo.");
            }

            return Ok(historial);
        }


        // ============================================================
        // CREAR HISTORIAL CREDITICIO
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(
            HistorialCrediticio historial)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (historial == null)
            {
                return BadRequest(
                    "Los datos del historial crediticio son obligatorios.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_repo.Insertar(historial))
            {
                return Ok(
                    "Historial crediticio agregado correctamente.");
            }

            return BadRequest(
                "No fue posible agregar el historial crediticio.");
        }


        // ============================================================
        // ACTUALIZAR HISTORIAL CREDITICIO
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(
            HistorialCrediticio historial)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (historial == null)
            {
                return BadRequest(
                    "Los datos del historial crediticio son obligatorios.");
            }

            if (historial.IdHistorial <= 0)
            {
                return BadRequest(
                    "El ID del historial no es válido.");
            }

            if (_repo.Actualizar(historial))
            {
                return Ok(
                    "Historial crediticio actualizado correctamente.");
            }

            return BadRequest(
                "No fue posible actualizar el historial crediticio.");
        }


        // ============================================================
        // ELIMINAR HISTORIAL CREDITICIO
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
                    "El ID del historial no es válido.");
            }

            if (_repo.Eliminar(id))
            {
                return Ok(
                    "Historial crediticio eliminado lógicamente.");
            }

            return BadRequest(
                "No fue posible eliminar el historial crediticio.");
        }


        // ============================================================
        // RESTAURAR HISTORIAL CREDITICIO
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
                    "El ID del historial no es válido.");
            }

            if (_repo.Restaurar(id))
            {
                return Ok(
                    "Historial crediticio restaurado correctamente.");
            }

            return BadRequest(
                "No fue posible restaurar el historial crediticio.");
        }
    }
}