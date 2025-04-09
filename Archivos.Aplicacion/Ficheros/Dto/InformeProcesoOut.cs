
using Archivos.Aplicacion.Comun;
using System.Diagnostics.CodeAnalysis;

namespace Archivos.Aplicacion.Ficheros.Dto
{
    [ExcludeFromCodeCoverage]
    public class InformeProcesoOut : BaseOut
    {
        public int TotalRegistros { get; set; }
        public int RegistrosExitosos { get; set; }
        public int RegistrosFallidos { get; set; }
    }
    
}
