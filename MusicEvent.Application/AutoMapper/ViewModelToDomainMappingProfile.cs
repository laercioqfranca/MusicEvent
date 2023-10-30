using AutoMapper;
using MusicEvent.Application.DTO;
using MusicEvent.Application.ViewModels.Auth;
using MusicEvent.Domain.Commands.Administracao;
using MusicEvent.Domain.Commands.Auth;
using MusicEvent.Domain.Commands.Evento;
using MusicEvent.Domain.Commands.Inscricao;

namespace MusicEvent.Application.AutoMapper
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