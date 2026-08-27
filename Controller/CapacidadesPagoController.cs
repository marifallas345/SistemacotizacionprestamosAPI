using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CapacidadesPagoController : ControllerBase
    {
        private readonly CapacidadPagoRepository _repo;

        public CapacidadesPagoController(
            CapacidadPagoRepository repo)
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

            var item =
                _repo.ObtenerPorId(id);

            if (item == null)
            {
                return NotFound(
                    "La capacidad de pago no existe o está inactiva.");
            }

            return Ok(item);
        }


        // ============================================================
        // INSERTAR
        // ============================================================

        [HttpPost]
        public IActionResult Post(
            CapacidadPago item)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (item.MontoMinimo < 0)
            {
                return BadRequest(
                    "El monto mínimo no puede ser negativo.");
            }

            if (item.MontoMaximo <= 0)
            {
                return BadRequest(
                    "El monto máximo debe ser mayor que cero.");
            }

            if (item.MontoMaximo < item.MontoMinimo)
            {
                return BadRequest(
                    "El monto máximo no puede ser menor que el monto mínimo.");
            }

            if (string.IsNullOrWhiteSpace(item.Descripcion))
            {
                return BadRequest(
                    "La descripción es obligatoria.");
            }

            return _repo.Insertar(item)
                ? Ok(
                    "Capacidad de pago agregada correctamente.")
                : BadRequest(
                    "No fue posible agregar la capacidad de pago.");
        }


        // ============================================================
        // ACTUALIZAR
        // ============================================================

        [HttpPut]
        public IActionResult Put(
            CapacidadPago item)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (item.IdCapacidadPago <= 0)
            {
                return BadRequest(
                    "El ID de la capacidad de pago no es válido.");
            }

            if (item.MontoMinimo < 0)
            {
                return BadRequest(
                    "El monto mínimo no puede ser negativo.");
            }

            if (item.MontoMaximo <= 0)
            {
                return BadRequest(
                    "El monto máximo debe ser mayor que cero.");
            }

            if (item.MontoMaximo < item.MontoMinimo)
            {
                return BadRequest(
                    "El monto máximo no puede ser menor que el monto mínimo.");
            }

            if (string.IsNullOrWhiteSpace(item.Descripcion))
            {
                return BadRequest(
                    "La descripción es obligatoria.");
            }

            return _repo.Actualizar(item)
                ? Ok(
                    "Capacidad de pago actualizada correctamente.")
                : BadRequest(
                    "No fue posible actualizar la capacidad de pago.");
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
                    "Capacidad de pago eliminada lógicamente.")
                : BadRequest(
                    "No fue posible eliminar la capacidad de pago.");
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
                    "Capacidad de pago restaurada correctamente.")
                : BadRequest(
                    "No fue posible restaurar la capacidad de pago.");
        }
    }
}