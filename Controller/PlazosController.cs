using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult Get() => Ok(_repo.Listar());

        [HttpGet("{id}")]
        public IActionResult Get(int id) => Ok(_repo.ObtenerPorId(id));

        [HttpPost]
        public IActionResult Post(Plazo item) =>
            _repo.Insertar(item) ? Ok("Plazo agregado correctamente.") : BadRequest();

        [HttpPut]
        public IActionResult Put(Plazo item) =>
            _repo.Actualizar(item) ? Ok("Plazo actualizado correctamente.") : BadRequest();

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _repo.Eliminar(id) ? Ok("Plazo eliminado correctamente.") : BadRequest();
    }
}