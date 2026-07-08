using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadosCotizacionController : ControllerBase
    {
        private readonly EstadoCotizacionRepository _repo;

        public EstadosCotizacionController(EstadoCotizacionRepository repo)
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
            return Ok(_repo.ObtenerPorId(id));
        }

        [HttpPost]
        public IActionResult Post(EstadoCotizacion estado)
        {
            if (_repo.Insertar(estado))
                return Ok("Estado de cotización agregado correctamente.");

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Put(EstadoCotizacion estado)
        {
            if (_repo.Actualizar(estado))
                return Ok("Estado de cotización actualizado correctamente.");

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_repo.Eliminar(id))
                return Ok("Estado de cotización eliminado correctamente.");

            return BadRequest();
        }
    }
}