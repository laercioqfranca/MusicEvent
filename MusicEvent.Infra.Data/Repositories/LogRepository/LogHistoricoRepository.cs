using MusicEvent.Domain.Interfaces.Infra.Data.Repositories.Auth;
using MusicEvent.Domain.Models.Administracao;
using MusicEvent.Infra.Data.Configuration;
using MusicEvent.Infra.Data.Context;

namespace MusicEvent.Infra.Data.Repositories.LogRepository
{
    public class LogHistoricoRepository : Repository<LogHistorico>, ILogHistoricoRepository
    {
        public LogHistoricoRepository(MusicEventContext dbContext) : base(dbContext)
        {
        }

    }
}
