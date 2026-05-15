using AutoMapper;
using GameHub.Application.Dtos.Genero;
using GameHub.Application.Dtos.Plataforma;
using GameHub.Application.Dtos.Usuario;
using GameHub.Application.Dtos.Videojuego;
using GameHub.Domain.Entities;


namespace GameHub.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Mapeo de Videojuego

             CreateMap<Videojuego, VideojuegoDto>()
                   .ForMember(dest => dest.NombreGenero,opt => opt.MapFrom(src => src.Genero.NombreGenero))
                   .ForMember(dest => dest.NombreDesarrollador,opt => opt.MapFrom(src => src.User!.NombreDesarrollador));

            CreateMap<VideojuegoCrearDto, Videojuego>();

            CreateMap<VideojuegoEditarDto, Videojuego>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            #region Mapeo de Genero

            CreateMap<Genero, GeneroDto>();
            CreateMap<GeneroCrearDto, Genero>();
            CreateMap<GeneroEditarDto, Genero>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            #endregion


            #region Mapeo de Plataforma
            CreateMap<Plataforma, PlataformaDto>();
            CreateMap<PlataformaCrearDto, Plataforma>();
            CreateMap<PlataformaEditarDto, Plataforma>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            #region Mapeo de Usuario

            CreateMap<ApplicationUser, UsuarioDto>()
                .ForMember(dest => dest.Rol, opt => opt.Ignore());

            CreateMap<UsuarioRegistroDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            #endregion
        }
    }
}
