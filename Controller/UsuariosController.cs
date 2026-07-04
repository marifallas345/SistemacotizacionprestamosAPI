using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Post(Usuario usuario)
        {
            return _repo.Insertar(usuario)
                ? Ok("Usuario agregado correctamente.")
                : BadRequest();
        }

        [HttpPut]
        public IActionResult Put(Usuario usuario)
        {
            return _repo.Actualizar(usuario)
                ? Ok("Usuario actualizado correctamente.")
                : BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _repo.Eliminar(id)
                ? Ok("Usuario eliminado correctamente.")
                : BadRequest();
        }
  
    [HttpPost("Login")]
        public IActionResult Login(LoginModel login)
        {
            var usuario = _repo.Login(login.NombreUsuario, login.Password);

            if (usuario == null)
            {
                return Unauthorized("Usuario o contraseña incorrectos.");
            }

            return Ok(usuario);
        }

    }
}