using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadosCotizacionController : ControllerBase
    {
        private readonly EstadoCotizacionRepository _repo;

        public EstadosCotizacionController(
            EstadoCotizacionRepository repo)
        {
            _repo = repo;
        }


        // ============================================================
        // LISTAR ESTADOS DE COTIZACIÓN
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpGet]
        public IActionResult Get()
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            return Ok(_repo.Listar());
        }


        // ============================================================
        // OBTENER ESTADO POR ID
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
                    "El ID del estado de cotización no es válido.");
            }

            var estado =
                _repo.ObtenerPorId(id);

            if (estado == null)
            {
                return NotFound(
                    "No se encontró el estado de cotización.");
            }

            return Ok(estado);
        }


        // ============================================================
        // CREAR ESTADO DE COTIZACIÓN
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(
            EstadoCotizacion estado)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (estado == null)
            {
                return BadRequest(
                    "Los datos del estado de cotización son obligatorios.");
            }

            if (_repo.Insertar(estado))
            {
                return Ok(
                    "Estado de cotización agregado correctamente.");
            }

            return BadRequest(
                "No fue posible agregar el estado de cotización.");
        }


        // ============================================================
        // ACTUALIZAR ESTADO DE COTIZACIÓN
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(
            EstadoCotizacion estado)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (estado == null)
            {
                return BadRequest(
                    "Los datos del estado de cotización son obligatorios.");
            }

            if (_repo.Actualizar(estado))
            {
                return Ok(
                    "Estado de cotización actualizado correctamente.");
            }

            return BadRequest(
                "No fue posible actualizar el estado de cotización.");
        }


        // ============================================================
        // ELIMINAR ESTADO DE COTIZACIÓN
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
                    "El ID del estado de cotización no es válido.");
            }

            if (_repo.Eliminar(id))
            {
                return Ok(
                    "Estado de cotización eliminado correctamente.");
            }

            return BadRequest(
                "No fue posible eliminar el estado de cotización.");
        }
    }
}