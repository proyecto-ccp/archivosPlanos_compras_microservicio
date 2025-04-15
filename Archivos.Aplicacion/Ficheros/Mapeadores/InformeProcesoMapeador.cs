

using Archivos.Aplicacion.Ficheros.Dto;
using Archivos.Dominio.Entidades;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace Archivos.Aplicacion.Ficheros.Mapeadores
{
    [ExcludeFromCodeCoverage]
    public class InformeProcesoMapeador : Profile
    {
        public InformeProcesoMapeador() 
        {
            CreateMap<InformeProceso, InformeProcesoOut>().ReverseMap();
        }
    }
    
}
