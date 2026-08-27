using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
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


        // ============================================================
        // LISTAR CLIENTES
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
        // OBTENER CLIENTE POR ID
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
                    "El ID del cliente no es válido.");
            }

            var cliente = _repo.ObtenerPorId(id);

            if (cliente == null)
            {
                return NotFound(
                    "No se encontró el cliente.");
            }

            return Ok(cliente);
        }


        // ============================================================
        // CREAR CLIENTE
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(Cliente cliente)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (cliente == null)
            {
                return BadRequest(
                    "Los datos del cliente son obligatorios.");
            }

            if (_repo.Insertar(cliente))
            {
                return Ok(
                    "Cliente agregado correctamente.");
            }

            return BadRequest(
                "No fue posible agregar el cliente.");
        }


        // ============================================================
        // ACTUALIZAR CLIENTE
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(Cliente cliente)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (cliente == null)
            {
                return BadRequest(
                    "Los datos del cliente son obligatorios.");
            }

            if (cliente.IdCliente <= 0)
            {
                return BadRequest(
                    "El ID del cliente no es válido.");
            }

            if (_repo.Actualizar(cliente))
            {
                return Ok(
                    "Cliente actualizado correctamente.");
            }

            return BadRequest(
                "No fue posible actualizar el cliente.");
        }


        // ============================================================
        // ELIMINAR CLIENTE
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
                    "El ID del cliente no es válido.");
            }

            if (_repo.Eliminar(id))
            {
                return Ok(
                    "Cliente eliminado correctamente.");
            }

            return BadRequest(
                "No fue posible eliminar el cliente.");
        }
    }
}