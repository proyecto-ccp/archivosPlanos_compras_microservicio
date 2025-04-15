
using Archivos.Aplicacion.Comun;
using Archivos.Aplicacion.Ficheros.Dto;
using Archivos.Dominio.Entidades;
using Archivos.Dominio.Servicios.Archivos;
using AutoMapper;
using MediatR;
using System.Net;

namespace Archivos.Aplicacion.Ficheros.Comandos
{
    public class ArchivoComandoHandler : IRequestHandler<ArchivoComando, InformeProcesoOut>
    {
        private readonly IMapper _mapper;
        private readonly LeerArchivo _leerArchivo;
        private readonly CrearProducto _crearProducto;
        public ArchivoComandoHandler(IMapper mapper, LeerArchivo leerArchivo, CrearProducto crearProducto)
        {
            _mapper = mapper;
            _leerArchivo = leerArchivo;
            _crearProducto = crearProducto;
        }
        public async Task<InformeProcesoOut> Handle(ArchivoComando request, CancellationToken cancellationToken)
        {
            InformeProcesoOut output = new();

            try
            {
                using var fileStream = request.file.OpenReadStream();
                var registros = await Task.Run(() => _leerArchivo.LeerArchivoCsv<RegistroCsv>(fileStream, ';'));

                var informe = await _crearProducto.Ejecutar(registros);

                output = _mapper.Map<InformeProcesoOut>(informe);
                output.Resultado = Resultado.Exitoso;
                output.Mensaje = "Operación exitosa";
                output.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                output.Resultado = Resultado.Error;
                output.Mensaje = string.Concat("Message: ", ex.Message, ex.InnerException is null ? "" : "-InnerException-" + ex.InnerException.Message);
                output.Status = HttpStatusCode.InternalServerError;
            }

            return output;
        }
    }
}
