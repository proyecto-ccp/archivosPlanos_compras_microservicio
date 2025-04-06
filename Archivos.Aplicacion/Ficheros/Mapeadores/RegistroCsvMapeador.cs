
using Archivos.Dominio.Entidades;
using AutoMapper;
using Ficheros.Aplicacion.Archivos.Dto;

namespace Archivos.Aplicacion.Ficheros.Mapeadores
{
    public class RegistroCsvMapeador : Profile
    {
        public RegistroCsvMapeador() 
        {
            CreateMap<RegistroCsv, RegistroCsvDto>().ReverseMap();
        }

    }
}
