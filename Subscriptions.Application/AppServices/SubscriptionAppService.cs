using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Subscriptions.Core.Interfaces;
using Subscriptions.Application.DTO;
using Subscriptions.Domain.Commands.Inscricao;
using Subscriptions.Domain.Interfaces.Infra.Data.Repositories;
using Subscriptions.Application.Interfaces;

namespace Subscriptions.Application.AppServices
{
    public class SubscriptionAppService : ISubscriptionAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IInscricaoRepository _repository;
        private readonly IHttpContextAccessor _httpContextAcessor;

        public SubscriptionAppService(IMapper mapper, IMediatorHandler bus, IInscricaoRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _bus = bus;
            _repository = repository;
            _httpContextAcessor = httpContextAccessor;
        }

        public async Task Create(InscricaoDTO InscricaoDTO)
        {
            var command = _mapper.Map<InscricaoCreateCommand>(InscricaoDTO);
            //command.UsuarioRequerenteId = Guid.Parse(_httpContextAcessor.HttpContext.User.Identity.Name);
            await _bus.SendCommand(command);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
