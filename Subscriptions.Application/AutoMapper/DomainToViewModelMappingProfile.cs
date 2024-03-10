using AutoMapper;
using Subscriptions.Application.ViewModels;
using Subscriptions.Application.ViewModels.Auth;
using Subscriptions.Domain.Models;
using Subscriptions.Domain.Models.Autenticacao;

namespace Subscriptions.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {        
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Usuario, LoginViewModel>().ReverseMap();

            CreateMap<Usuario, UsuarioViewModel>();
            CreateMap<PerfilUsuario, PerfilUsuarioViewModel>();

            CreateMap<Evento, EventoViewModel>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data.ToString("dd/MM/yyyy")));

        }
    }
}
