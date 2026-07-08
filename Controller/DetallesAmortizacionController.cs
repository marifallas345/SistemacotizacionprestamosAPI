using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallesAmortizacionController : ControllerBase
    {
        private readonly DetalleAmortizacionRepository _repo;

        public DetallesAmortizacionController(DetalleAmortizacionRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.Listar());
        }

        [HttpGet("porCotizacion/{idCotizacion}")]
        public IActionResult GetPorCotizacion(int idCotizacion)
        {
            return Ok(_repo.ListarPorCotizacion(idCotizacion));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            DetalleAmortizacion? detalle = _repo.ObtenerPorId(id);

            if (detalle == null)
                return NotFound("El detalle no existe o está inactivo.");

            return Ok(detalle);
        }

        [HttpPost]
        public IActionResult Post(DetalleAmortizacion detalle)
        {
            if (_repo.Insertar(detalle))
                return Ok("Detalle de amortización agregado correctamente.");

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Put(DetalleAmortizacion detalle)
        {
            if (_repo.Actualizar(detalle))
                return Ok("Detalle de amortización actualizado correctamente.");

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_repo.Eliminar(id))
                return Ok("Detalle de amortización eliminado correctamente.");

            return BadRequest();
        }
    }
}