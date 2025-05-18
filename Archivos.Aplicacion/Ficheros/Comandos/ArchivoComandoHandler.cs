
using Archivos.Aplicacion.Comun;
using Archivos.Aplicacion.Ficheros.Dto;
using Archivos.Dominio.Entidades;
using Archivos.Dominio.ObjetoValor;
using Archivos.Dominio.Puertos.Integraciones;
using Archivos.Dominio.Servicios.Archivos;
using AutoMapper;
using MediatR;
using System.Net;
using System.Text.Json;

namespace Archivos.Aplicacion.Ficheros.Comandos
{
    public class ArchivoComandoHandler : IRequestHandler<ArchivoComando, InformeProcesoOut>
    {
        private readonly IMapper _mapper;
        private readonly LeerArchivo _leerArchivo;
        private readonly CrearProducto _crearProducto;
        private readonly IServicioAuditoriaApi _servicioAuditoriaApi;
        public ArchivoComandoHandler(IMapper mapper, LeerArchivo leerArchivo, CrearProducto crearProducto, IServicioAuditoriaApi servicioAuditoriaApi)
        {
            _mapper = mapper;
            _leerArchivo = leerArchivo;
            _crearProducto = crearProducto;
            _servicioAuditoriaApi = servicioAuditoriaApi;
        }
        public async Task<InformeProcesoOut> Handle(ArchivoComando request, CancellationToken cancellationToken)
        {
            InformeProcesoOut output = new();

            try
            {
                using var fileStream = request.File.OpenReadStream();
                var registros = await Task.Run(() => _leerArchivo.LeerArchivoCsv<RegistroCsv>(fileStream, ';'));

                var informe = await _crearProducto.Ejecutar(registros, request.Control.Token);

                output = _mapper.Map<InformeProcesoOut>(informe);
                output.Resultado = Resultado.Exitoso;
                output.Mensaje = "Operación exitosa";
                output.Status = HttpStatusCode.OK;

                var inputAuditoria = _mapper.Map<Auditoria>(request);
                inputAuditoria.IdRegistro = "No Aplica";
                inputAuditoria.Registro = JsonSerializer.Serialize(output);
                _ = Task.Run(() => _servicioAuditoriaApi.RegistrarAuditoria(inputAuditoria), cancellationToken);
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
