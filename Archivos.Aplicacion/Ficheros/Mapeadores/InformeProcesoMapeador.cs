

using Archivos.Aplicacion.Ficheros.Comandos;
using Archivos.Aplicacion.Ficheros.Dto;
using Archivos.Dominio.Entidades;
using Archivos.Dominio.ObjetoValor;
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

            CreateMap<ArchivoComando, Auditoria>()
                .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.Control.IdUsuario))
                .ForMember(dest => dest.Accion, opt => opt.MapFrom(src => "Archivo procesado"))
                .ForMember(dest => dest.TablaAfectada, opt => opt.MapFrom(src => "tbl_productos"));
        }
    }
    
}
