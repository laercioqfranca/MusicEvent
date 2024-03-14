using AutoMapper;
using Microsoft.AspNetCore.Http;
using Log.Application.DTO;
using Log.Application.Interfaces;
using Log.Core.Interfaces;
using Log.Domain.Interfaces.Infra.Data.Repositories;
using Log.Domain.Commands.Inscricao;

namespace Log.Application.AppServices
{
    public class LogAppService : ILogAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IInscricaoRepository _repository;
        private readonly IHttpContextAccessor _httpContextAcessor;

        public LogAppService(IMapper mapper, IMediatorHandler bus, IInscricaoRepository repository, IHttpContextAccessor httpContextAccessor)
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
