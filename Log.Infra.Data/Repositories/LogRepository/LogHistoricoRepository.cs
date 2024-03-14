using Log.Domain.Interfaces.Infra.Data.Repositories;
using Log.Domain.Models.Administracao;
using Log.Infra.Data.Configuration;
using Log.Infra.Data.Context;

namespace Log.Infra.Data.Repositories.LogRepository
{
    public class LogHistoricoRepository : Repository<LogHistorico>, ILogHistoricoRepository
    {
        public LogHistoricoRepository(LogContext dbContext) : base(dbContext)
        {
        }

    }
}
