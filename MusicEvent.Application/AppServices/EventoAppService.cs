using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MusicEvent.Application.Interfaces;
using MusicEvent.Application.ViewModels;
using MusicEvent.Core.Interfaces;
using MusicEvent.Domain.Interfaces.Infra.Data.Repositories;

namespace MusicEvent.Application.AppServices
{
    public class EventoAppService : IEventoAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IEventoRepository _repository;

        public EventoAppService(IMapper mapper, IMediatorHandler bus, IEventoRepository repository)
        {
            _mapper = mapper;
            _bus = bus;
            _repository = repository;
        }

        public async Task<IEnumerable<EventoViewModel>> GetAll()
        {
            var query = await _repository.GetAll();
            var list = _mapper.Map<List<EventoViewModel>>(query);
            return list;

        }

    }

}
