
using Archivos.Dominio.Puertos.Integraciones;

namespace Archivos.Dominio.Servicios.Archivos
{
    public class LeerArchivo(IArchivoPlano archivoIntegracion)
    {
        private readonly IArchivoPlano _archivo = archivoIntegracion;

        public List<T> LeerArchivoCsv<T>(Stream fileStream, char delimiter = ';') where T : class, new()
        {
            if (fileStream == null)
            {
                throw new ArgumentNullException(nameof(fileStream), "El archivo no puede ser nulo");
            }

            if (fileStream.Length == 0)
            {
                throw new ArgumentException("El archivo no puede estar vacío", nameof(fileStream));
            }

            return _archivo.LeerArchivoCsv<T>(fileStream, delimiter);

        }


    }
}
