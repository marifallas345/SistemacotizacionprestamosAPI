using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NivelesEducativosController : ControllerBase
    {
        private readonly NivelEducativoRepository _repo;

        public NivelesEducativosController(NivelEducativoRepository repo)
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
        public IActionResult Post(NivelEducativo nivel)
        {
            return _repo.Insertar(nivel)
                ? Ok("Nivel educativo agregado correctamente.")
                : BadRequest();
        }

        [HttpPut]
        public IActionResult Put(NivelEducativo nivel)
        {
            return _repo.Actualizar(nivel)
                ? Ok("Nivel educativo actualizado correctamente.")
                : BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _repo.Eliminar(id)
                ? Ok("Nivel educativo eliminado correctamente.")
                : BadRequest();
        }
    }
}