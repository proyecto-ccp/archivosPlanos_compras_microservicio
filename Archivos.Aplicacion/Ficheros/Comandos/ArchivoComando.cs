using Archivos.Aplicacion.Ficheros.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Archivos.Aplicacion.Ficheros.Comandos
{
    [ExcludeFromCodeCoverage]
    public record ArchivoComando(
        IFormFile file
        ) : IRequest<InformeProcesoOut>;
}
