using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediosContratacionController : ControllerBase
    {
        private readonly MedioContratacionRepository _repo;

        public MediosContratacionController(MedioContratacionRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_repo.Listar());

        [HttpGet("{id}")]
        public IActionResult Get(int id) => Ok(_repo.ObtenerPorId(id));

        [HttpPost]
        public IActionResult Post(MedioContratacion item) =>
            _repo.Insertar(item) ? Ok("Medio de contratación agregado correctamente.") : BadRequest();

        [HttpPut]
        public IActionResult Put(MedioContratacion item) =>
            _repo.Actualizar(item) ? Ok("Medio de contratación actualizado correctamente.") : BadRequest();

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _repo.Eliminar(id) ? Ok("Medio de contratación eliminado correctamente.") : BadRequest();
    }
}