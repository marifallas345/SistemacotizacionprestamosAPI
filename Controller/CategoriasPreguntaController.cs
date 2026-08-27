using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Models;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasPreguntaController : ControllerBase
    {
        private readonly CategoriaPreguntaRepository _repo;

        public CategoriasPreguntaController(
            CategoriaPreguntaRepository repo)
        {
            _repo = repo;
        }


        // ============================================================
        // LISTAR CATEGORÍAS
        // ADMINISTRADOR + ENCUESTADOR
        // ============================================================

        [HttpGet]
        public IActionResult Get(
            [FromQuery] bool incluirInactivos = false)
        {
            // El Encuestador puede consultar categorías activas.
            // El Administrador puede consultar activas e inactivas.

            if (AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Ok(
                    _repo.Listar(incluirInactivos));
            }

            if (AutorizacionApiHelper.EsEncuestador(Request))
            {
                // El Encuestador NO puede ver eliminadas.
                if (incluirInactivos)
                {
                    return Forbid();
                }

                return Ok(
                    _repo.Listar(false));
            }

            return Forbid();
        }


        // ============================================================
        // OBTENER CATEGORÍA POR ID
        // ADMINISTRADOR + ENCUESTADOR
        // ============================================================

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request) &&
                !AutorizacionApiHelper.EsEncuestador(Request))
            {
                return Forbid();
            }

            if (id <= 0)
            {
                return BadRequest(
                    "El ID de la categoría no es válido.");
            }

            var categoria =
                _repo.ObtenerPorId(id);

            if (categoria == null)
            {
                return NotFound(
                    "La categoría no existe o está inactiva.");
            }

            return Ok(categoria);
        }


        // ============================================================
        // CREAR CATEGORÍA
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPost]
        public IActionResult Post(
            CategoriaPregunta categoria)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (categoria == null)
            {
                return BadRequest(
                    "Los datos de la categoría son obligatorios.");
            }

            if (string.IsNullOrWhiteSpace(categoria.Nombre))
            {
                return BadRequest(
                    "El nombre de la categoría es obligatorio.");
            }

            if (_repo.Insertar(categoria))
            {
                return Ok(
                    "Categoría agregada correctamente.");
            }

            return BadRequest(
                "No fue posible agregar la categoría.");
        }


        // ============================================================
        // ACTUALIZAR CATEGORÍA
        // SOLO ADMINISTRADOR
        // ============================================================

        [HttpPut]
        public IActionResult Put(
            CategoriaPregunta categoria)
        {
            if (!AutorizacionApiHelper.EsAdministrador(Request))
            {
                return Forbid();
            }

            if (categoria == null)
            {
                return BadRequest(
                    "Los datos de la categoría son obligatorios.");
            }

            if (categoria.IdCategoria <= 0)
            {
                return BadRequest(
                    "El ID de la categoría no es válido.");
            }

            if (string.IsNullOrWhiteSpace(categoria.Nombre))
            {
                return BadRequest(
                    "El nombre de la categoría es obligatorio.");
            }

            if (_repo.Actualizar(categoria))
            {
                return Ok(
                    "Categoría actualizada correctamente.");
            }

            return BadRequest(
                "No fue posible actualizar la categoría.");
        }


        // ============================================================
        // ELIMINAR CATEGORÍA
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
                    "El ID de la categoría no es válido.");
            }

            if (_repo.Eliminar(id))
            {
                return Ok(
                    "Categoría eliminada lógicamente.");
            }

            return BadRequest(
                "No fue posible eliminar la categoría.");
        }


        // ============================================================
        // RESTAURAR CATEGORÍA
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
                    "El ID de la categoría no es válido.");
            }

            if (_repo.Restaurar(id))
            {
                return Ok(
                    "Categoría restaurada correctamente.");
            }

            return BadRequest(
                "No fue posible restaurar la categoría.");
        }
    }
}