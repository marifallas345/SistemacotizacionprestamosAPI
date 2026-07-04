using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteRepository _repo;

        public ClientesController(ClienteRepository repo)
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
        public IActionResult Post(Cliente cliente)
        {
            return _repo.Insertar(cliente)
                ? Ok("Cliente agregado correctamente.")
                : BadRequest();
        }

        [HttpPut]
        public IActionResult Put(Cliente cliente)
        {
            return _repo.Actualizar(cliente)
                ? Ok("Cliente actualizado correctamente.")
                : BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _repo.Eliminar(id)
                ? Ok("Cliente eliminado correctamente.")
                : BadRequest();
        }
    }
}