using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CotizacionesController : ControllerBase
    {
        private readonly CotizacionRepository _repo;

        public CotizacionesController(CotizacionRepository repo)
        {
            _repo = repo;
        }


        // ============================================================
        // LISTAR COTIZACIONES
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
        // OBTENER COTIZACIÓN POR ID
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
                    "El ID de la cotización no es válido.");
            }

            Cotizacion? cotizacion =
                _repo.ObtenerPorId(id);

            if (cotizacion == null)
            {
                return NotFound(
                    "La cotización no existe o está inactiva.");
            }

            return Ok(cotizacion);
        }


        // ============================================================
        // INSERTAR COTIZACIÓN
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(Cotizacion cotizacion)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (cotizacion == null)
            {
                return BadRequest(
                    "Los datos de la cotización son obligatorios.");
            }

            if (cotizacion.IdCliente <= 0)
            {
                return BadRequest(
                    "El cliente de la cotización no es válido.");
            }

            if (cotizacion.IdTipoPrestamo <= 0)
            {
                return BadRequest(
                    "El tipo de préstamo no es válido.");
            }

            if (cotizacion.IdPlazo <= 0)
            {
                return BadRequest(
                    "El plazo de la cotización no es válido.");
            }

            if (cotizacion.MontoSolicitado <= 0)
            {
                return BadRequest(
                    "El monto solicitado debe ser mayor que cero.");
            }

            int idGenerado =
                _repo.Insertar(cotizacion);

            if (idGenerado > 0)
            {
                return Ok(new
                {
                    Mensaje =
                        "Cotización generada correctamente.",

                    IdCotizacion =
                        idGenerado
                });
            }

            return BadRequest(
                "No fue posible generar la cotización.");
        }


        // ============================================================
        // ACTUALIZAR COTIZACIÓN
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(Cotizacion cotizacion)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (cotizacion == null)
            {
                return BadRequest(
                    "Los datos de la cotización son obligatorios.");
            }

            if (cotizacion.IdCotizacion <= 0)
            {
                return BadRequest(
                    "El ID de la cotización no es válido.");
            }

            if (cotizacion.IdEstado <= 0)
            {
                return BadRequest(
                    "El estado de la cotización no es válido.");
            }

            if (cotizacion.MontoSolicitado <= 0)
            {
                return BadRequest(
                    "El monto solicitado debe ser mayor que cero.");
            }

            if (_repo.Actualizar(cotizacion))
            {
                return Ok(
                    "Cotización actualizada correctamente.");
            }

            return BadRequest(
                "No fue posible actualizar la cotización.");
        }


        // ============================================================
        // ELIMINAR COTIZACIÓN
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
                    "El ID de la cotización no es válido.");
            }

            if (_repo.Eliminar(id))
            {
                return Ok(
                    "Cotización eliminada correctamente.");
            }

            return BadRequest(
                "No fue posible eliminar la cotización.");
        }


        // ============================================================
        // RESTAURAR COTIZACIÓN
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
                    "El ID de la cotización no es válido.");
            }

            return _repo.Restaurar(id)
                ? Ok(
                    "Cotización restaurada correctamente.")
                : BadRequest(
                    "No fue posible restaurar la cotización.");
        }
    }
}