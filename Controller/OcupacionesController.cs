using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OcupacionesController : ControllerBase
    {
        private readonly OcupacionRepository _repo;

        public OcupacionesController(OcupacionRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.Listar());
        }
    }
}