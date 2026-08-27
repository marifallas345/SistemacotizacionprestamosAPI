using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallesAmortizacionController : ControllerBase
    {
        private readonly DetalleAmortizacionRepository _repo;

        public DetallesAmortizacionController(
            DetalleAmortizacionRepository repo)
        {
            _repo = repo;
        }


        // ============================================================
        // LISTAR DETALLES DE AMORTIZACIÓN
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
        // LISTAR AMORTIZACIÓN POR COTIZACIÓN
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpGet("porCotizacion/{idCotizacion:int}")]
        public IActionResult GetPorCotizacion(
            int idCotizacion)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (idCotizacion <= 0)
            {
                return BadRequest(
                    "El ID de la cotización no es válido.");
            }

            return Ok(
                _repo.ListarPorCotizacion(idCotizacion));
        }


        // ============================================================
        // OBTENER DETALLE POR ID
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
                    "El ID del detalle no es válido.");
            }

            DetalleAmortizacion? detalle =
                _repo.ObtenerPorId(id);

            if (detalle == null)
            {
                return NotFound(
                    "El detalle no existe o está inactivo.");
            }

            return Ok(detalle);
        }


        // ============================================================
        // CREAR DETALLE DE AMORTIZACIÓN
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(
            DetalleAmortizacion detalle)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (detalle == null)
            {
                return BadRequest(
                    "Los datos del detalle de amortización son obligatorios.");
            }

            if (_repo.Insertar(detalle))
            {
                return Ok(
                    "Detalle de amortización agregado correctamente.");
            }

            return BadRequest(
                "No fue posible agregar el detalle de amortización.");
        }


        // ============================================================
        // ACTUALIZAR DETALLE DE AMORTIZACIÓN
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(
            DetalleAmortizacion detalle)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (detalle == null)
            {
                return BadRequest(
                    "Los datos del detalle de amortización son obligatorios.");
            }

            if (_repo.Actualizar(detalle))
            {
                return Ok(
                    "Detalle de amortización actualizado correctamente.");
            }

            return BadRequest(
                "No fue posible actualizar el detalle de amortización.");
        }


        // ============================================================
        // ELIMINAR DETALLE DE AMORTIZACIÓN
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
                    "El ID del detalle no es válido.");
            }

            if (_repo.Eliminar(id))
            {
                return Ok(
                    "Detalle de amortización eliminado correctamente.");
            }

            return BadRequest(
                "No fue posible eliminar el detalle de amortización.");
        }
    }
}