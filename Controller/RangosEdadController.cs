using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RangosEdadController : ControllerBase
    {
        private readonly RangoEdadRepository _repo;

        public RangosEdadController(RangoEdadRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_repo.Listar());

        [HttpGet("{id}")]
        public IActionResult Get(int id) => Ok(_repo.ObtenerPorId(id));

        [HttpPost]
        public IActionResult Post(RangoEdad item) =>
            _repo.Insertar(item) ? Ok("Rango de edad agregado correctamente.") : BadRequest();

        [HttpPut]
        public IActionResult Put(RangoEdad item) =>
            _repo.Actualizar(item) ? Ok("Rango de edad actualizado correctamente.") : BadRequest();

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _repo.Eliminar(id) ? Ok("Rango de edad eliminado correctamente.") : BadRequest();
    }
}