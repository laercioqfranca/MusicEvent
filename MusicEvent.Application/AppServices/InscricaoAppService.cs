using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using MusicEvent.Core.Interfaces;
using MusicEvent.Application.DTO;
using MusicEvent.Domain.Commands.Inscricao;
using MusicEvent.Domain.Interfaces.Infra.Data.Repositories;
using MusicEvent.Application.Interfaces;
using MusicEvent.Application.ViewModels;

namespace MusicEvent.Application.AppServices
{
    public class InscricaoAppService : IInscricaoAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IInscricaoRepository _repository;
        private readonly IHttpContextAccessor _httpContextAcessor;

        public InscricaoAppService(IMapper mapper, IMediatorHandler bus, IInscricaoRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _bus = bus;
            _repository = repository;
            _httpContextAcessor = httpContextAccessor;
        }

        public async Task<IEnumerable<EventoViewModel>> GetAllById(Guid id)
        {
            var query = await _repository.GetAllById(id);
            var evento = query.Select(x => x.Evento).OrderBy(x => x.Data);
            return _mapper.Map<IEnumerable<EventoViewModel>>(evento);
        }

        public async Task Create(InscricaoDTO InscricaoDTO)
        {
            var command = _mapper.Map<InscricaoCreateCommand>(InscricaoDTO);
            command.UsuarioRequerenteId = Guid.Parse(_httpContextAcessor.HttpContext.User.Identity.Name);
            await _bus.SendCommand(command);
        }

        public async Task Delete(Guid id)
        {
            var command = new InscricaoDeleteCommand(id);
            command.UsuarioRequerenteId = Guid.Parse(_httpContextAcessor.HttpContext.User.Identity.Name);
            await _bus.SendCommand(command);

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
