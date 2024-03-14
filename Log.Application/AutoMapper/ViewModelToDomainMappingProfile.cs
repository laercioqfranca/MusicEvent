using AutoMapper;
using Log.Application.DTO;
using Log.Application.ViewModels;
using Log.Domain.Commands;

namespace Log.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<LogViewModel, LogCreateCommand>();
        }
    }
}