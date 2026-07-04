using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RangosIngresosController : ControllerBase
    {
        private readonly RangoIngresoRepository _repo;

        public RangosIngresosController(RangoIngresoRepository repo)
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
        public IActionResult Post(RangoIngreso rango)
        {
            return _repo.Insertar(rango)
                ? Ok("Rango de ingresos agregado correctamente.")
                : BadRequest();
        }

        [HttpPut]
        public IActionResult Put(RangoIngreso rango)
        {
            return _repo.Actualizar(rango)
                ? Ok("Rango de ingresos actualizado correctamente.")
                : BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _repo.Eliminar(id)
                ? Ok("Rango de ingresos eliminado correctamente.")
                : BadRequest();
        }
    }
}