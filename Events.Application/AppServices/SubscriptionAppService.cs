﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Events.Core.Interfaces;
using Events.Application.DTO;
using Events.Domain.Commands.Inscricao;
using Events.Domain.Interfaces.Infra.Data.Repositories;
using Events.Application.Interfaces;
using Events.Application.ViewModels;

namespace Events.Application.AppServices
{
    public class SubscriptionAppService : ISubscriptionAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly ISubscriptionRepository _repository;
        private readonly IHttpContextAccessor _httpContextAcessor;

        public SubscriptionAppService(IMapper mapper, IMediatorHandler bus, ISubscriptionRepository repository, IHttpContextAccessor httpContextAccessor)
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

        public async Task Create(SubscriptionDTO InscricaoDTO)
        {
            var command = _mapper.Map<SubscriptionCreateCommand>(InscricaoDTO);
            command.UsuarioRequerenteId = Guid.Parse(_httpContextAcessor.HttpContext.User.Identity.Name);
            await _bus.SendCommand(command);
        }

        public async Task Delete(Guid id)
        {
            var command = new SubscriptionDeleteCommand(id);
            command.UsuarioRequerenteId = Guid.Parse(_httpContextAcessor.HttpContext.User.Identity.Name);
            await _bus.SendCommand(command);

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}