using AutoMapper;
using Subscriptions.Application.DTO;
using Subscriptions.Domain.Commands.Inscricao;

namespace Subscriptions.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<InscricaoDTO, InscricaoCreateCommand>();
        }
    }
}