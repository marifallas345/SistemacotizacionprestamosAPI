using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CapacidadesPagoController : ControllerBase
    {
        private readonly CapacidadPagoRepository _repo;

        public CapacidadesPagoController(CapacidadPagoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_repo.Listar());

        [HttpGet("{id}")]
        public IActionResult Get(int id) => Ok(_repo.ObtenerPorId(id));

        [HttpPost]
        public IActionResult Post(CapacidadPago item) =>
            _repo.Insertar(item) ? Ok("Capacidad de pago agregada correctamente.") : BadRequest();

        [HttpPut]
        public IActionResult Put(CapacidadPago item) =>
            _repo.Actualizar(item) ? Ok("Capacidad de pago actualizada correctamente.") : BadRequest();

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _repo.Eliminar(id) ? Ok("Capacidad de pago eliminada correctamente.") : BadRequest();
    }
}