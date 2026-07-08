using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Cotizacion? cotizacion = _repo.ObtenerPorId(id);

            if (cotizacion == null)
                return NotFound("La cotización no existe o está inactiva.");

            return Ok(cotizacion);
        }

        [HttpPost]
        public IActionResult Post(Cotizacion cotizacion)
        {
            int idGenerado = _repo.Insertar(cotizacion);

            if (idGenerado > 0)
                return Ok(new { Mensaje = "Cotización generada correctamente.", IdCotizacion = idGenerado });

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Put(Cotizacion cotizacion)
        {
            if (_repo.Actualizar(cotizacion))
                return Ok("Cotización actualizada correctamente.");

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_repo.Eliminar(id))
                return Ok("Cotización eliminada correctamente.");

            return BadRequest();
        }
    }
}