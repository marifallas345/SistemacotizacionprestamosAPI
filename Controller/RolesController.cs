using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
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


        // ============================================================
        // LISTAR ROLES
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpGet]
        public IActionResult Get()
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            return Ok(_repo.Listar());
        }


        // ============================================================
        // OBTENER ROL POR ID
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (id <= 0)
            {
                return BadRequest(
                    "El ID del rol no es válido.");
            }

            var rol = _repo.ObtenerPorId(id);

            if (rol == null)
            {
                return NotFound(
                    "No se encontró el rol.");
            }

            return Ok(rol);
        }


        // ============================================================
        // CREAR ROL
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(Rol rol)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (rol == null)
            {
                return BadRequest(
                    "Los datos del rol son obligatorios.");
            }

            if (_repo.Insertar(rol))
            {
                return Ok(
                    "Rol agregado correctamente.");
            }

            return BadRequest(
                "No fue posible agregar el rol.");
        }


        // ============================================================
        // ACTUALIZAR ROL
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(Rol rol)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (rol == null)
            {
                return BadRequest(
                    "Los datos del rol son obligatorios.");
            }

            if (rol.IdRol <= 0)
            {
                return BadRequest(
                    "El ID del rol no es válido.");
            }

            if (_repo.Actualizar(rol))
            {
                return Ok(
                    "Rol actualizado correctamente.");
            }

            return BadRequest(
                "No fue posible actualizar el rol.");
        }


        // ============================================================
        // ELIMINAR ROL
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (id <= 0)
            {
                return BadRequest(
                    "El ID del rol no es válido.");
            }

            if (_repo.Eliminar(id))
            {
                return Ok(
                    "Rol eliminado correctamente.");
            }

            return BadRequest(
                "No fue posible eliminar el rol.");
        }
    }
}