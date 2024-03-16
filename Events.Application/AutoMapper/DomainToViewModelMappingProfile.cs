using AutoMapper;
using Events.Application.ViewModels;
using Events.Application.ViewModels.Auth;
using Events.Domain.Models;
using Events.Domain.Models.Autenticacao;

namespace Events.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {        
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Usuario, LoginViewModel>().ReverseMap();

            CreateMap<Usuario, UsuarioViewModel>();
            CreateMap<PerfilUsuario, PerfilUsuarioViewModel>();

            CreateMap<Eventos, EventoViewModel>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data.ToString("dd/MM/yyyy")));

        }
    }
}
