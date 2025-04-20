

using Archivos.Dominio.Entidades;
using Archivos.Dominio.ObjetoValor;
using Archivos.Dominio.Puertos.Integraciones;

namespace Archivos.Dominio.Servicios.Archivos
{
    public class CrearProducto(IServicioProductosApi servicioProductosApi)
    {
        private readonly IServicioProductosApi _servicioProductosApi = servicioProductosApi;
        
        public async Task<InformeProceso> Ejecutar(List<RegistroCsv> registros)
        {
            var informe = new InformeProceso
            {
                TotalRegistros = registros.Count,
                RegistrosExitosos = 0,
                RegistrosFallidos = 0
            };

            foreach (var registro in registros)
            {
                var producto = new Producto
                {
                    Nombre = registro.Nombre,
                    Descripcion = registro.Descripcion,
                    IdProveedor = registro.IdProveedor,
                    PrecioUnitario = registro.PrecioUnitario,
                    IdMedida = registro.IdMedida,
                    IdCategoria = registro.IdCategoria,
                    IdMarca = registro.IdMarca,
                    IdColor = registro.IdColor,
                    IdModelo = registro.IdModelo,
                    IdMaterial = registro.IdMaterial,
                    UrlFoto1 = registro.UrlFoto1,
                    UrlFoto2 = registro.UrlFoto2,
                    Cantidad = registro.Cantidad
                };

                var resultadoOperacion = await _servicioProductosApi.CrearProducto(producto);
                
                if (resultadoOperacion.Resultado == 1)
                {
                    informe.RegistrosExitosos++;
                }
                else
                {
                    informe.RegistrosFallidos++;
                }
            }

            return informe;
        }
    }
}
