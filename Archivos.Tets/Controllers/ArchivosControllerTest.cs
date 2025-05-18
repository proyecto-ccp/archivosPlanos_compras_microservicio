
using Archivos.Aplicacion.Comun;
using Archivos.Aplicacion.Ficheros.Comandos;
using Archivos.Aplicacion.Ficheros.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServicioArchivos.Api.Controllers;
using System.Net;

namespace Archivos.Tets.Controllers
{
    public class ArchivosControllerTest
    {
        private readonly Mock<IMediator> mockMediator;

        public ArchivosControllerTest()
        {
            mockMediator = new Mock<IMediator>();
        }

        [Theory]
        [InlineData(Resultado.Exitoso, HttpStatusCode.OK)]
        [InlineData(Resultado.Error, HttpStatusCode.InternalServerError)]
        public async Task EnviarPlanoCsv_Respuestas(Resultado enumRes, HttpStatusCode status)
        {
            var output = new InformeProcesoOut
            {
                Resultado = enumRes,
                Status = status
            };

            mockMediator.Setup(m => m.Send(It.IsAny<ArchivoComando>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(output);

            var objPrueba = new ArchivosController(mockMediator.Object);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Authorization = "Bearer pruebas-token-123";
            httpContext.Items["UserId"] = Guid.NewGuid().ToString();
            objPrueba.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            var baseIn = new BaseIn
            {
                Token = "tokenPruebasUnitarias",
                IdUsuario = Guid.NewGuid().ToString(),
            };

            var file = new Mock<IFormFile>();
            var request = new ArchivoComando(file.Object, baseIn);

            var resultado = await objPrueba.EnviarPlanoCsv(request);

            if (enumRes == Resultado.Exitoso)
            {
                var verResultado = Assert.IsType<OkObjectResult>(resultado);
                var res = verResultado.Value as InformeProcesoOut;
                Assert.IsType<InformeProcesoOut>(res);
                Assert.Equal(200, verResultado.StatusCode);
            }
            else if (enumRes == Resultado.Error)
            {
                var verResultado = Assert.IsType<ObjectResult>(resultado);
                var res = verResultado.Value as ProblemDetails;
                Assert.IsType<ProblemDetails>(res);
                Assert.Equal(500, verResultado.StatusCode);
            }

        }

    }
}
