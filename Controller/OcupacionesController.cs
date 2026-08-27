using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OcupacionesController : ControllerBase
    {
        private readonly OcupacionRepository _repo;

        public OcupacionesController(
            OcupacionRepository repo)
        {
            _repo = repo;
        }


        // ============================================================
        // LISTAR OCUPACIONES
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

            // Solo el Administrador puede solicitar
            // registros inactivos.
            if (incluirInactivos &&
                !AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            return Ok(
                _repo.Listar(incluirInactivos));
        }


        // ============================================================
        // OBTENER OCUPACIÓN POR ID
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
                    "El ID de la ocupación no es válido.");
            }

            var ocupacion =
                _repo.ObtenerPorId(id);

            if (ocupacion == null)
            {
                return NotFound(
                    "La ocupación no existe.");
            }

            return Ok(ocupacion);
        }


        // ============================================================
        // CREAR OCUPACIÓN
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(
            Ocupacion ocupacion)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (ocupacion == null)
            {
                return BadRequest(
                    "Los datos de la ocupación son obligatorios.");
            }

            if (string.IsNullOrWhiteSpace(
                ocupacion.Nombre))
            {
                return BadRequest(
                    "El nombre de la ocupación es obligatorio.");
            }

            return _repo.Insertar(ocupacion)
                ? Ok(
                    "Ocupación agregada correctamente.")
                : BadRequest(
                    "No fue posible agregar la ocupación.");
        }


        // ============================================================
        // ACTUALIZAR OCUPACIÓN
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(
            Ocupacion ocupacion)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (ocupacion == null)
            {
                return BadRequest(
                    "Los datos de la ocupación son obligatorios.");
            }

            if (ocupacion.IdOcupacion <= 0)
            {
                return BadRequest(
                    "El ID de la ocupación no es válido.");
            }

            if (string.IsNullOrWhiteSpace(
                ocupacion.Nombre))
            {
                return BadRequest(
                    "El nombre de la ocupación es obligatorio.");
            }

            return _repo.Actualizar(ocupacion)
                ? Ok(
                    "Ocupación actualizada correctamente.")
                : BadRequest(
                    "No fue posible actualizar la ocupación.");
        }


        // ============================================================
        // ELIMINACIÓN LÓGICA
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
                    "El ID de la ocupación no es válido.");
            }

            return _repo.Eliminar(id)
                ? Ok(
                    "Ocupación eliminada lógicamente.")
                : BadRequest(
                    "No fue posible eliminar la ocupación.");
        }


        // ============================================================
        // RESTAURAR OCUPACIÓN
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
                    "El ID de la ocupación no es válido.");
            }

            return _repo.Restaurar(id)
                ? Ok(
                    "Ocupación restaurada correctamente.")
                : BadRequest(
                    "No fue posible restaurar la ocupación.");
        }
    }
}