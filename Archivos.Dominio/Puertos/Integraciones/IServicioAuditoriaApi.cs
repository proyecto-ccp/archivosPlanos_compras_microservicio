using Archivos.Dominio.ObjetoValor;

namespace Archivos.Dominio.Puertos.Integraciones
{
    public interface IServicioAuditoriaApi
    {
        Task RegistrarAuditoria(Auditoria auditoria);
    }
}
