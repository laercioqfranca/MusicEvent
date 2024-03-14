using AutoMapper;
using Log.Application.DTO;
using Log.Domain.Commands.Inscricao;

namespace Log.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<InscricaoDTO, InscricaoCreateCommand>();
        }
    }
}