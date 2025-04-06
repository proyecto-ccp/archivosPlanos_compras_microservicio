using Archivos.Aplicacion.Comun;
using Archivos.Aplicacion.Ficheros.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Archivos.Aplicacion.Ficheros.Comandos
{
    public record ArchivoComando(
        IFormFile file
        ) : IRequest<InformeProcesoOut>;
}
