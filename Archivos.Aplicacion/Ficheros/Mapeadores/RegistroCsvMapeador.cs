
using Archivos.Dominio.Entidades;
using AutoMapper;
using Ficheros.Aplicacion.Archivos.Dto;
using System.Diagnostics.CodeAnalysis;

namespace Archivos.Aplicacion.Ficheros.Mapeadores
{
    [ExcludeFromCodeCoverage]
    public class RegistroCsvMapeador : Profile
    {
        public RegistroCsvMapeador() 
        {
            CreateMap<RegistroCsv, RegistroCsvDto>().ReverseMap();
        }

    }
}
