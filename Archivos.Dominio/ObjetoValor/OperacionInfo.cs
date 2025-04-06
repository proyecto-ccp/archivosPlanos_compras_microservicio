using System.Net;

namespace Archivos.Dominio.ObjetoValor
{
    public class OperacionInfo
    {
        public int Resultado { get; set; }
        public string Mensaje { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
