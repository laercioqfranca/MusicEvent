using AutoMapper;
using Events.Application.DTO;
using Events.Application.ViewModels.Auth;
using Events.Domain.Commands.Administracao;
using Events.Domain.Commands.Auth;
using Events.Domain.Commands.Evento;
using Events.Domain.Commands.Inscricao;

namespace Events.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<LoginViewModel, AutenticarCommand>();

            CreateMap<UsuarioDTO, UsuarioCreateCommand>();

            CreateMap<UsuarioViewModel, UsuarioUpdateCommand>();

            CreateMap<SubscriptionDTO, SubscriptionCreateCommand>();

            CreateMap<EventoDTO, EventoCreateCommand>();

            CreateMap<EventoDTO, EventoUpdateCommand>();
        }
    }
}