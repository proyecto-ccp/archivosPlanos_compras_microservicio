
using Archivos.Dominio.ObjetoValor;

namespace Archivos.Dominio.Puertos.Integraciones
{
    public interface IServicioUsuariosApi
    {
        Task<TokenInfo> ValidarToken(string token);
    }
}
