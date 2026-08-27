using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _repo;

        public UsuariosController(UsuarioRepository repo)
        {
            _repo = repo;
        }

        // ============================================================
        // LISTAR USUARIOS
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
        // OBTENER USUARIO
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            var usuario = _repo.ObtenerPorId(id);

            if (usuario == null)
            {
                return NotFound(
                    "No se encontró el usuario.");
            }

            return Ok(usuario);
        }


        // ============================================================
        // CREAR USUARIO
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            int idUsuario =
                _repo.Insertar(usuario);

            if (idUsuario <= 0)
            {
                return BadRequest(
                    "No fue posible agregar el usuario.");
            }

            return Ok(idUsuario);
        }


        // ============================================================
        // ACTUALIZAR USUARIO
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(Usuario usuario)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            return _repo.Actualizar(usuario)
                ? Ok("Usuario actualizado correctamente.")
                : BadRequest(
                    "No fue posible actualizar el usuario.");
        }


        // ============================================================
        // ELIMINAR USUARIO
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            return _repo.Eliminar(id)
                ? Ok("Usuario eliminado correctamente.")
                : BadRequest(
                    "No fue posible eliminar el usuario.");
        }


        // ============================================================
        // LOGIN
        // ESTE ENDPOINT NO REQUIERE ROL
        // ============================================================

        [HttpPost("Login")]
        public IActionResult Login(LoginModel login)
        {
            var usuario =
                _repo.Login(
                    login.NombreUsuario,
                    login.Password);

            if (usuario == null)
            {
                return Unauthorized(
                    "Usuario o contraseña incorrectos.");
            }

            return Ok(usuario);
        }
    }
}