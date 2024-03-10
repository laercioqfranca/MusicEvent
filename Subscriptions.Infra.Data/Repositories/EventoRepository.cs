using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Subscriptions.Domain.Interfaces.Infra.Data.Repositories;
using Subscriptions.Domain.Models;
using Subscriptions.Infra.Data.Configuration;
using Subscriptions.Infra.Data.Context;

namespace Subscriptions.Infra.Data.Repositories
{
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        private readonly SubscriptionsContext _context;
        public EventoRepository(SubscriptionsContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Evento>> GetAll()
        {
            IQueryable<Evento> query = DbSet.Where(x => !x.Excluido).OrderBy(x => x.Data);
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<Evento> GetById(Guid idEvento)
        {
            var evento = await _context.Set<Evento>()
                .Where(
                    x => x.Id == idEvento && !x.Excluido
            ).FirstOrDefaultAsync();
            return evento;
        }
    }
}
