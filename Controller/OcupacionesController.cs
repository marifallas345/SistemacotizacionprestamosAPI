using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OcupacionesController : ControllerBase
    {
        private readonly OcupacionRepository _repo;

        public OcupacionesController(OcupacionRepository repo)
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
        public IActionResult Post(Ocupacion ocupacion)
        {
            return _repo.Insertar(ocupacion)
                ? Ok("Ocupación agregada correctamente.")
                : BadRequest();
        }

        [HttpPut]
        public IActionResult Put(Ocupacion ocupacion)
        {
            return _repo.Actualizar(ocupacion)
                ? Ok("Ocupación actualizada correctamente.")
                : BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _repo.Eliminar(id)
                ? Ok("Ocupación eliminada correctamente.")
                : BadRequest();
        }
    }
}