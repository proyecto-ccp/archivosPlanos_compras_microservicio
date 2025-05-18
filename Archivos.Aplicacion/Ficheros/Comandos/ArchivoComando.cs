using Archivos.Aplicacion.Comun;
using Archivos.Aplicacion.Ficheros.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Archivos.Aplicacion.Ficheros.Comandos
{
    [ExcludeFromCodeCoverage]
    public record ArchivoComando(
        IFormFile File,
        BaseIn Control
        ) : IRequest<InformeProcesoOut>;
}
