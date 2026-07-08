using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiposPrestamoController : ControllerBase
    {
        private readonly TipoPrestamoRepository _repo;

        public TiposPrestamoController(TipoPrestamoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_repo.Listar());

        [HttpGet("{id}")]
        public IActionResult Get(int id) => Ok(_repo.ObtenerPorId(id));

        [HttpPost]
        public IActionResult Post(TipoPrestamo item) =>
            _repo.Insertar(item) ? Ok("Tipo de préstamo agregado correctamente.") : BadRequest();

        [HttpPut]
        public IActionResult Put(TipoPrestamo item) =>
            _repo.Actualizar(item) ? Ok("Tipo de préstamo actualizado correctamente.") : BadRequest();

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _repo.Eliminar(id) ? Ok("Tipo de préstamo eliminado correctamente.") : BadRequest();
    }
}
