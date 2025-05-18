using Archivos.Dominio.ObjetoValor;

namespace Archivos.Dominio.Puertos.Integraciones
{
    public interface IServicioProductosApi
    {
        Task<OperacionInfo> CrearProducto(Producto producto, string token);
    }
}
