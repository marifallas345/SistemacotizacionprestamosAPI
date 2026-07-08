using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MontosRangoController : ControllerBase
    {
        private readonly MontoRangoRepository _repo;

        public MontosRangoController(MontoRangoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_repo.Listar());

        [HttpGet("{id}")]
        public IActionResult Get(int id) => Ok(_repo.ObtenerPorId(id));

        [HttpPost]
        public IActionResult Post(MontoRango item) =>
            _repo.Insertar(item) ? Ok("Rango de monto agregado correctamente.") : BadRequest();

        [HttpPut]
        public IActionResult Put(MontoRango item) =>
            _repo.Actualizar(item) ? Ok("Rango de monto actualizado correctamente.") : BadRequest();

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _repo.Eliminar(id) ? Ok("Rango de monto eliminado correctamente.") : BadRequest();
    }
}