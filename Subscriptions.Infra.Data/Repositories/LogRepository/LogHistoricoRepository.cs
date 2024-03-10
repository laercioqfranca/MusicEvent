using Subscriptions.Domain.Interfaces.Infra.Data.Repositories.Auth;
using Subscriptions.Domain.Models.Administracao;
using Subscriptions.Infra.Data.Configuration;
using Subscriptions.Infra.Data.Context;

namespace Subscriptions.Infra.Data.Repositories.LogRepository
{
    public class LogHistoricoRepository : Repository<LogHistorico>, ILogHistoricoRepository
    {
        public LogHistoricoRepository(SubscriptionsContext dbContext) : base(dbContext)
        {
        }

    }
}
