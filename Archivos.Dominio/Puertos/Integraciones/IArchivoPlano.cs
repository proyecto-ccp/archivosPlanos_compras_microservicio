

namespace Archivos.Dominio.Puertos.Integraciones
{
    public interface IArchivoPlano
    {
        List<T> LeerArchivoCsv<T>(Stream fileStream, char delimiter = ';') where T : class, new();
    }
}
