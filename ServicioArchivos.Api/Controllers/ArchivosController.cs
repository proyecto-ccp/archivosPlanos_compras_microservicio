using Archivos.Aplicacion.Comun;
using Archivos.Aplicacion.Ficheros.Comandos;
using Archivos.Aplicacion.Ficheros.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ServicioArchivos.Api.Controllers
{
    /// <summary>
    /// Controlador para procesar archivos de productos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ArchivosController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        public ArchivosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Procesar archivo CSV de productos
        /// </summary>
        /// <response code="200"> 
        /// InformeProcesoOut pendiente
        /// </response>
        [HttpPost]
        [Route("EnviarPlanoCsv")]
        [ProducesResponseType(typeof(InformeProcesoOut), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> EnviarPlanoCsv([FromForm] ArchivoComando plano)
        {
            var output = await _mediator.Send(plano);

            if (output.Resultado != Resultado.Error)
            {
                return Ok(output);
            }
            else
            {
                return Problem(output.Mensaje, statusCode: (int)output.Status);
            }

        }
    }
}
