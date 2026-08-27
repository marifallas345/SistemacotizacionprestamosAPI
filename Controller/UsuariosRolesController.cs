using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosRolesController : ControllerBase
    {
        private readonly UsuarioRolRepository _repo;

        public UsuariosRolesController(
            UsuarioRolRepository repo)
        {
            _repo = repo;
        }


        // ============================================================
        // LISTAR RELACIONES USUARIO - ROL
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpGet]
        public IActionResult Get(
            [FromQuery] bool incluirInactivos = false)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            return Ok(
                _repo.Listar(incluirInactivos));
        }


        // ============================================================
        // OBTENER RELACIÓN USUARIO - ROL POR ID
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
                    "El ID de la relación usuario-rol no es válido.");
            }

            var usuarioRol =
                _repo.ObtenerPorId(id);

            if (usuarioRol == null)
            {
                return NotFound(
                    "La relación usuario-rol no existe o está inactiva.");
            }

            return Ok(usuarioRol);
        }


        // ============================================================
        // CREAR RELACIÓN USUARIO - ROL
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(
            UsuarioRol usuarioRol)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (usuarioRol == null)
            {
                return BadRequest(
                    "Los datos de la relación usuario-rol son obligatorios.");
            }

            if (usuarioRol.IdUsuario <= 0)
            {
                return BadRequest(
                    "Debe indicar un usuario válido.");
            }

            if (usuarioRol.IdRol <= 0)
            {
                return BadRequest(
                    "Debe indicar un rol válido.");
            }

            if (_repo.Insertar(usuarioRol))
            {
                return Ok(
                    "Relación usuario-rol agregada correctamente.");
            }

            return BadRequest(
                "No fue posible agregar la relación usuario-rol.");
        }


        // ============================================================
        // ACTUALIZAR RELACIÓN USUARIO - ROL
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(
            UsuarioRol usuarioRol)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (usuarioRol == null)
            {
                return BadRequest(
                    "Los datos de la relación usuario-rol son obligatorios.");
            }

            if (usuarioRol.IdUsuarioRol <= 0)
            {
                return BadRequest(
                    "El ID de la relación no es válido.");
            }

            if (usuarioRol.IdUsuario <= 0)
            {
                return BadRequest(
                    "Debe indicar un usuario válido.");
            }

            if (usuarioRol.IdRol <= 0)
            {
                return BadRequest(
                    "Debe indicar un rol válido.");
            }

            if (_repo.Actualizar(usuarioRol))
            {
                return Ok(
                    "Relación usuario-rol actualizada correctamente.");
            }

            return BadRequest(
                "No fue posible actualizar la relación usuario-rol.");
        }


        // ============================================================
        // ELIMINAR RELACIÓN USUARIO - ROL
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
                    "El ID de la relación no es válido.");
            }

            if (_repo.Eliminar(id))
            {
                return Ok(
                    "Relación usuario-rol eliminada lógicamente.");
            }

            return BadRequest(
                "No fue posible eliminar la relación usuario-rol.");
        }


        // ============================================================
        // RESTAURAR RELACIÓN USUARIO - ROL
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut("Restaurar/{id:int}")]
        public IActionResult Restaurar(int id)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (id <= 0)
            {
                return BadRequest(
                    "El ID de la relación no es válido.");
            }

            if (_repo.Restaurar(id))
            {
                return Ok(
                    "Relación usuario-rol restaurada correctamente.");
            }

            return BadRequest(
                "No fue posible restaurar la relación usuario-rol.");
        }
    }
}