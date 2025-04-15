
using Archivos.Dominio.ObjetoValor;
using Archivos.Dominio.Puertos.Integraciones;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;

namespace Archivos.Infraestructura.Adaptadores.Integraciones
{
    [ExcludeFromCodeCoverage]
    public class ServicioProductosApi : IServicioProductosApi
    {
        private readonly HttpClient _httpClient;

        public ServicioProductosApi(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            string _baseUrl = configuration["ServicioProductos:UrlBase"] ?? throw new InvalidOperationException("ServicioProductos:UrlBase no ha sido configurada");
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }
        public async Task<OperacionInfo> CrearProducto(Producto producto)
        {
            var respuesta = await _httpClient.PostAsJsonAsync("api/Productos/Crear", producto);
            respuesta.EnsureSuccessStatusCode();

            var operacionInfo = await respuesta.Content.ReadFromJsonAsync<OperacionInfo>();

            return operacionInfo;
        }
    }
}
