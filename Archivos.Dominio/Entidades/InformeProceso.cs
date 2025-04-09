
using System.Diagnostics.CodeAnalysis;

namespace Archivos.Dominio.Entidades
{
    [ExcludeFromCodeCoverage]
    public class InformeProceso
    {
        public int TotalRegistros { get; set; }
        public int RegistrosExitosos { get; set; }
        public int RegistrosFallidos { get; set; }

    }
}
