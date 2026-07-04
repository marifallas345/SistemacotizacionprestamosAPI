using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RolRepository _repo;

        public RolesController(RolRepository repo)
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
        public IActionResult Post(Rol rol)
        {
            if (_repo.Insertar(rol))
                return Ok("Rol agregado correctamente.");

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Put(Rol rol)
        {
            if (_repo.Actualizar(rol))
                return Ok("Rol actualizado correctamente.");

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_repo.Eliminar(id))
                return Ok("Rol eliminado correctamente.");

            return BadRequest();
        }
    }
}