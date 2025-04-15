
using Archivos.Aplicacion.Comun;
using Archivos.Aplicacion.Ficheros.Dto;
using Archivos.Dominio.Entidades;
using Archivos.Dominio.ObjetoValor;
using System.Net;


namespace Archivos.Tets.DataTests
{
    public class ArchivoComandoHandlerDataTest : TheoryData<List<RegistroCsv>, OperacionInfo, InformeProcesoOut>
    {
        public ArchivoComandoHandlerDataTest() 
        {
            List<RegistroCsv> registros =
            [
                new ()
                        {
                            Nombre = "Producto 1",
                            Descripcion = "Descripción del producto 1",
                            IdProveedor = Guid.NewGuid(),
                            PrecioUnitario = 10.0m,
                            IdMedida = 1,
                            IdCategoria = 1,
                            IdMarca = 1,
                            IdColor = 1,
                            IdModelo = 1,
                            IdMaterial = 1,
                            UrlFoto1 = "http://example.com/foto1.jpg",
                            UrlFoto2 = "http://example.com/foto2.jpg"
                        },
                        new ()
                        {
                            Nombre = "Producto 2",
                            Descripcion = "Descripción del producto 2",
                            IdProveedor = Guid.NewGuid(),
                            PrecioUnitario = 20.0m,
                            IdMedida = 2,
                            IdCategoria = 2,
                            IdMarca = 2,
                            IdColor = 2,
                            IdModelo = 2,
                            IdMaterial = 2,
                            UrlFoto1 = "http://example.com/foto3.jpg",
                            UrlFoto2 = "http://example.com/foto4.jpg"
                        }
            ];

            var respuestaOk = new OperacionInfo
            {
                Resultado = (int)Resultado.Exitoso,
                Mensaje = "Operación exitosa",
                Status = HttpStatusCode.OK,
            };

            var respuestaError = new OperacionInfo
            {
                Resultado = (int)Resultado.Error,
                Mensaje = "Error",
                Status = HttpStatusCode.InternalServerError,
            };

            var validacion1 = new InformeProcesoOut
            {
                TotalRegistros = 2,
                RegistrosExitosos = 2,
                RegistrosFallidos = 0,
                Mensaje = "Operación exitosa",
                Resultado = Resultado.Exitoso,
                Status = HttpStatusCode.OK
            };

            var validacion2 = new InformeProcesoOut
            {
                TotalRegistros = 2,
                RegistrosExitosos = 0,
                RegistrosFallidos = 2,
                Mensaje = "Operación exitosa",
                Resultado = Resultado.Exitoso,
                Status = HttpStatusCode.OK
            };

            Add(registros, respuestaOk, validacion1);
            Add(registros, respuestaError, validacion2);

        }

    }
}
