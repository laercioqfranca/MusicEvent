using AutoMapper;
using MusicEvent.Application.ViewModels;
using MusicEvent.Application.ViewModels.Auth;
using MusicEvent.Domain.Models;
using MusicEvent.Domain.Models.Autenticacao;

namespace MusicEvent.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {        
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Usuario, LoginViewModel>().ReverseMap();

            CreateMap<Usuario, UsuarioViewModel>();
            CreateMap<PerfilUsuario, PerfilUsuarioViewModel>();

            CreateMap<Eventos, EventoViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
                //.ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data.ToString("dd/MM/yyyy")));

        }
    }
}
