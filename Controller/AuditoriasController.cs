using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditoriasController : ControllerBase
    {
        private readonly AuditoriaRepository _repo;

        public AuditoriasController(AuditoriaRepository repo)
        {
            _repo = repo;
        }


        // ============================================================
        // LISTAR AUDITORÍAS
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            // --------------------------------------------------------
            // Validar que exista una sesión de Administrador
            // --------------------------------------------------------

            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            try
            {
                List<AuditoriaModel> auditorias =
                    _repo.Listar();

                return Ok(auditorias);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        mensaje =
                            "Error al listar auditorías.",
                        detalle =
                            ex.Message
                    });
            }
        }
    }
}