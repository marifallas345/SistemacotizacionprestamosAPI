using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasasInteresRangoController : ControllerBase
    {
        private readonly TasaInteresRangoRepository _repo;

        public TasasInteresRangoController(TasaInteresRangoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_repo.Listar());

        [HttpGet("{id}")]
        public IActionResult Get(int id) => Ok(_repo.ObtenerPorId(id));

        [HttpPost]
        public IActionResult Post(TasaInteresRango item) =>
            _repo.Insertar(item) ? Ok("Rango de tasa agregado correctamente.") : BadRequest();

        [HttpPut]
        public IActionResult Put(TasaInteresRango item) =>
            _repo.Actualizar(item) ? Ok("Rango de tasa actualizado correctamente.") : BadRequest();

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _repo.Eliminar(id) ? Ok("Rango de tasa eliminado correctamente.") : BadRequest();
    }
}