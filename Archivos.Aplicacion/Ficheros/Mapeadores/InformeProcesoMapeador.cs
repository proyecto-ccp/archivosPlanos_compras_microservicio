

using Archivos.Aplicacion.Ficheros.Dto;
using Archivos.Dominio.Entidades;
using AutoMapper;

namespace Archivos.Aplicacion.Ficheros.Mapeadores
{
    public class InformeProcesoMapeador : Profile
    {
        public InformeProcesoMapeador() 
        {
            CreateMap<InformeProceso, InformeProcesoOut>().ReverseMap();
        }
    }
    
}
