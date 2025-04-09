using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Archivos.Dominio.ObjetoValor
{
    [ExcludeFromCodeCoverage]
    public class OperacionInfo
    {
        public int Resultado { get; set; }
        public string Mensaje { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
