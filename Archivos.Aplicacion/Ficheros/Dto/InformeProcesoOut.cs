
using Archivos.Aplicacion.Comun;

namespace Archivos.Aplicacion.Ficheros.Dto
{
    public class InformeProcesoOut : BaseOut
    {
        public int TotalRegistros { get; set; }
        public int RegistrosExitosos { get; set; }
        public int RegistrosFallidos { get; set; }
    }
    
}
