using AutoMapper;
using Microsoft.AspNetCore.Http;
using Log.Application.Interfaces;
using Log.Core.Interfaces;
using Log.Domain.Interfaces.Infra.Data.Repositories;
using Log.Application.ViewModels;
using Log.Domain.Commands;

namespace Log.Application.AppServices
{
    public class LogAppService : ILogAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly ILogHistoricoRepository _repository;
        private readonly IHttpContextAccessor _httpContextAcessor;

        public LogAppService(IMapper mapper, IMediatorHandler bus, ILogHistoricoRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _bus = bus;
            _repository = repository;
            _httpContextAcessor = httpContextAccessor;
        }

        public async Task CreateLog(LogViewModel logViewModel)
        {
            var command = _mapper.Map<LogCreateCommand>(logViewModel);
            await _bus.SendCommand(command);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
