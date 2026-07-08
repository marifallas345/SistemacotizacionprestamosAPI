using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EncuestasController : ControllerBase
    {
        private readonly EncuestaRepository _repo;

        public EncuestasController(EncuestaRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("GuardarCompleta")]
        public IActionResult GuardarCompleta(EncuestaCompletaModel encuesta)
        {
            try
            {
                if (_repo.GuardarEncuestaCompleta(encuesta))
                    return Ok("Encuesta almacenada correctamente.");

                return BadRequest("No fue posible almacenar la encuesta.");
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    "Error al guardar la encuesta: " + ex.Message);
            }
        }

        [HttpGet("Contador")]
        public IActionResult Contador()
        {
            try
            {
                return Ok(_repo.ContarEncuestasActivas());
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    "Error al contar encuestas: " + ex.Message);
            }
        }

        [HttpGet("Listar")]
        public IActionResult Listar(bool incluirEliminados = false)
        {
            try
            {
                return Ok(_repo.ListarEncuestas(incluirEliminados));
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    "Error al listar encuestas: " + ex.Message);
            }
        }

        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                if (_repo.EliminarEncuesta(id))
                    return Ok("Encuesta eliminada correctamente.");

                return NotFound("No se encontró la encuesta.");
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    "Error al eliminar encuesta: " + ex.Message);
            }
        }

        [HttpPut("Restaurar/{id}")]
        public IActionResult Restaurar(int id)
        {
            try
            {
                if (_repo.RestaurarEncuesta(id))
                    return Ok("Encuesta restaurada correctamente.");

                return NotFound("No se encontró la encuesta.");
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    "Error al restaurar encuesta: " + ex.Message);
            }
        }

        [HttpGet("BuscarPorNombre/{nombre}")]
        public IActionResult BuscarPorNombre(string nombre)
        {
            try
            {
                var encuesta = _repo.BuscarEncuestaPorNombre(nombre);

                if (encuesta == null)
                    return NotFound("No se encontró la encuesta.");

                return Ok(encuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    "Error al buscar encuesta: " + ex.Message);
            }
        }
    }
}