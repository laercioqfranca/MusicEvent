using AutoMapper;
using MusicEvent.Application.ViewModels.Auth;
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

        }
    }
}
