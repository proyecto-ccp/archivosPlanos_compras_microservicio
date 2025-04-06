using Archivos.Dominio.Puertos.Integraciones;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Archivos.Infraestructura.Adaptadores.Integraciones
{
    public class ArchivoPlano : IArchivoPlano
    {
        public List<T> LeerArchivoCsv<T>(Stream fileStream, char delimiter = ';') where T : class, new()
        {
            using var reader = new StreamReader(fileStream);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = delimiter.ToString()
            };
            using var csvReader = new CsvReader(reader, config);
            return csvReader.GetRecords<T>().ToList();
        }
    }
}
