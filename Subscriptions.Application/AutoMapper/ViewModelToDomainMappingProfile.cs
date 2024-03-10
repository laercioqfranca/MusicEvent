using AutoMapper;
using Subscriptions.Application.DTO;
using Subscriptions.Application.ViewModels.Auth;
using Subscriptions.Domain.Commands.Administracao;
using Subscriptions.Domain.Commands.Auth;
using Subscriptions.Domain.Commands.Evento;
using Subscriptions.Domain.Commands.Inscricao;

namespace Subscriptions.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<LoginViewModel, AutenticarCommand>();

            CreateMap<UsuarioDTO, UsuarioCreateCommand>();

            CreateMap<UsuarioViewModel, UsuarioUpdateCommand>();

            CreateMap<InscricaoDTO, InscricaoCreateCommand>();

            CreateMap<EventoDTO, EventoCreateCommand>();

            CreateMap<EventoDTO, EventoUpdateCommand>();
        }
    }
}