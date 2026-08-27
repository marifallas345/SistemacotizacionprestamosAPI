using Microsoft.AspNetCore.Mvc;
using SistemacotizacionprestamosAPI.Helpers;
using SistemacotizacionprestamosAPI.Repositories;

namespace SistemacotizacionprestamosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly ReporteRepository _repo;

        public ReportesController(ReporteRepository repo)
        {
            _repo = repo;
        }



        private IActionResult Ejecutar(
            int numero,
            int idRangoEdad,
            int idRangoIngresos,
            int idGenero)
        {

            if (!AutorizacionApiHelper.EsAdministrador(Request) &&
                !AutorizacionApiHelper.EsConsultor(Request))
            {
                return Forbid();
            }

            try
            {
                var resultado =
                    _repo.EjecutarReporte(
                        numero,
                        idRangoEdad,
                        idRangoIngresos,
                        idGenero);

                return Ok(resultado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        mensaje =
                            "Error al ejecutar el reporte.",

                        detalle =
                            ex.Message
                    });
            }
        }



        [HttpGet("Pregunta1")]
        public IActionResult Pregunta1(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                1,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }


        [HttpGet("Pregunta2")]
        public IActionResult Pregunta2(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                2,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }



        [HttpGet("Pregunta3")]
        public IActionResult Pregunta3(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                3,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }


        [HttpGet("Pregunta4")]
        public IActionResult Pregunta4(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                4,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }



        [HttpGet("Pregunta5")]
        public IActionResult Pregunta5(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                5,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }


        [HttpGet("Pregunta6")]
        public IActionResult Pregunta6(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                6,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }


        [HttpGet("Pregunta7")]
        public IActionResult Pregunta7(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                7,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }


        [HttpGet("Pregunta8")]
        public IActionResult Pregunta8(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                8,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }



        [HttpGet("Pregunta9")]
        public IActionResult Pregunta9(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                9,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }



        [HttpGet("Pregunta10")]
        public IActionResult Pregunta10(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                10,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }


        // ============================================================
        // REPORTE PREGUNTA 11
        // ============================================================

        [HttpGet("Pregunta11")]
        public IActionResult Pregunta11(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                11,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }



        [HttpGet("Pregunta12")]
        public IActionResult Pregunta12(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                12,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }


        [HttpGet("Pregunta13")]
        public IActionResult Pregunta13(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                13,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }


        [HttpGet("Pregunta14")]
        public IActionResult Pregunta14(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                14,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }

        [HttpGet("Pregunta15")]
        public IActionResult Pregunta15(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                15,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }


        [HttpGet("Pregunta16")]
        public IActionResult Pregunta16(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                16,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }




        [HttpGet("Pregunta17")]
        public IActionResult Pregunta17(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                17,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }



        [HttpGet("Pregunta18")]
        public IActionResult Pregunta18(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                18,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }



        [HttpGet("Pregunta19")]
        public IActionResult Pregunta19(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                19,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }



        [HttpGet("Pregunta20")]
        public IActionResult Pregunta20(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                20,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }



        [HttpGet("Pregunta21")]
        public IActionResult Pregunta21(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                21,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }




        [HttpGet("Pregunta22")]
        public IActionResult Pregunta22(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                22,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }




        [HttpGet("Pregunta23")]
        public IActionResult Pregunta23(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                23,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }




        [HttpGet("Pregunta24")]
        public IActionResult Pregunta24(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                24,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }



        [HttpGet("Pregunta25")]
        public IActionResult Pregunta25(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                25,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }



        [HttpGet("Pregunta26")]
        public IActionResult Pregunta26(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                26,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }



        [HttpGet("Pregunta27")]
        public IActionResult Pregunta27(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                27,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }


        [HttpGet("Pregunta28")]
        public IActionResult Pregunta28(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                28,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }




        [HttpGet("Pregunta29")]
        public IActionResult Pregunta29(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                29,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }

        [HttpGet("Pregunta30")]
        public IActionResult Pregunta30(
            int idRangoEdad = 0,
            int idRangoIngresos = 0,
            int idGenero = 0)
        {
            return Ejecutar(
                30,
                idRangoEdad,
                idRangoIngresos,
                idGenero);
        }
    }
}