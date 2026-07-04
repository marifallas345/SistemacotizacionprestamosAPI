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
            if (_repo.GuardarEncuestaCompleta(encuesta))
                return Ok("Encuesta almacenada correctamente.");

            return BadRequest("No fue posible almacenar la encuesta.");
        }
    }
}