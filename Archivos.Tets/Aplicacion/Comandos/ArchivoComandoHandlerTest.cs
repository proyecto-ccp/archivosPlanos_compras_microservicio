
using Archivos.Aplicacion.Comun;
using Archivos.Aplicacion.Ficheros.Comandos;
using Archivos.Aplicacion.Ficheros.Dto;
using Archivos.Dominio.Entidades;
using Archivos.Dominio.ObjetoValor;
using Archivos.Dominio.Puertos.Integraciones;
using Archivos.Dominio.Servicios.Archivos;
using Archivos.Tets.DataTests;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Net;

namespace Archivos.Tets.Aplicacion.Comandos
{
    public class ArchivoComandoHandlerTest
    {
        private readonly LeerArchivo _leerArchivo;
        private readonly CrearProducto _crearProducto;
        private readonly IMapper _mapper;
        private readonly Mock<IArchivoPlano> mockIArchivo;
        private readonly Mock<IServicioProductosApi> mockServicioProductosApi;

        public ArchivoComandoHandlerTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegistroCsv, Producto>();
                cfg.CreateMap<InformeProceso, InformeProcesoOut>();
            });
            _mapper = config.CreateMapper();
            mockIArchivo = new Mock<IArchivoPlano>();
            mockServicioProductosApi = new Mock<IServicioProductosApi>();
            _leerArchivo = new LeerArchivo(mockIArchivo.Object);
            _crearProducto = new CrearProducto(mockServicioProductosApi.Object);
        }

        /// <summary>
        /// valida las repuestas cuando se procesa el plano y se crea el producto
        /// </summary>
        [Theory]
        [ClassData(typeof(ArchivoComandoHandlerDataTest))]
        public async Task CrearArchivoComandoMock(List<RegistroCsv> registros, OperacionInfo resServicio, InformeProcesoOut validacion)
        {
            var fileMock = new Mock<IFormFile>();
            var content = "Contenido de prueba";
            var fileName = "test.csv";
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            await writer.WriteAsync(content);
            await writer.FlushAsync();
            memoryStream.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(memoryStream);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(memoryStream.Length);
            
            var archivoComando = new ArchivoComando(fileMock.Object);

            mockIArchivo.Setup(x => x.LeerArchivoCsv<RegistroCsv>(It.IsAny<Stream>(), It.IsAny<char>())).Returns(registros);

            mockServicioProductosApi.Setup(x => x.CrearProducto(It.IsAny<Producto>())).ReturnsAsync(resServicio);

            var objPrueba = new ArchivoComandoHandler(_mapper, _leerArchivo, _crearProducto);

            var resultado = await objPrueba.Handle(archivoComando, CancellationToken.None);

            var verResultado = Assert.IsType<InformeProcesoOut>(resultado);
            Assert.Equal(validacion.Mensaje, verResultado.Mensaje);
            Assert.Equal(validacion.Resultado, verResultado.Resultado);
            Assert.Equal(validacion.Status, verResultado.Status);
            Assert.Equal(validacion.TotalRegistros, verResultado.TotalRegistros);
            Assert.Equal(validacion.RegistrosExitosos, verResultado.RegistrosExitosos);
            Assert.Equal(validacion.RegistrosFallidos, verResultado.RegistrosFallidos);
        }

        [Fact]
        public async Task CrearArchivo_RespuestaArchivoNulo()
        {
            var fileMock = new Mock<IFormFile>();

            var archivoComando = new ArchivoComando(fileMock.Object);

            mockIArchivo.Setup(x => x.LeerArchivoCsv<RegistroCsv>(It.IsAny<Stream>(), It.IsAny<char>())).Returns([]);

            var objPrueba = new ArchivoComandoHandler(_mapper, _leerArchivo, _crearProducto);

            var resultado = await objPrueba.Handle(archivoComando, CancellationToken.None);

            var verResultado = Assert.IsType<InformeProcesoOut>(resultado);
            Assert.Contains("El archivo no puede ser nulo", verResultado.Mensaje);
            Assert.Equal(Resultado.Error, verResultado.Resultado);
            Assert.Equal(HttpStatusCode.InternalServerError, verResultado.Status);
        }

        [Fact]
        public async Task CrearArchivo_RespuestaArchivoVacio()
        {
            var fileMock = new Mock<IFormFile>();
            var content = "";
            var fileName = "test.csv";
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            await writer.WriteAsync(content);
            await writer.FlushAsync();
            memoryStream.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(memoryStream);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(memoryStream.Length);

            var archivoComando = new ArchivoComando(fileMock.Object);

            mockIArchivo.Setup(x => x.LeerArchivoCsv<RegistroCsv>(It.IsAny<Stream>(), It.IsAny<char>())).Returns([]);

            var objPrueba = new ArchivoComandoHandler(_mapper, _leerArchivo, _crearProducto);

            var resultado = await objPrueba.Handle(archivoComando, CancellationToken.None);

            var verResultado = Assert.IsType<InformeProcesoOut>(resultado);
            Assert.Contains("El archivo no puede estar vacío", verResultado.Mensaje);
            Assert.Equal(Resultado.Error, verResultado.Resultado);
            Assert.Equal(HttpStatusCode.InternalServerError, verResultado.Status);
        }

    }
}
