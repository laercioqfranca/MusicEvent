using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Events.Domain.Interfaces.Infra.Data.Repositories;
using Events.Domain.Models;
using Events.Infra.Data.Configuration;
using Events.Infra.Data.Context;

namespace Events.Infra.Data.Repositories
{
    public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
    {
        private readonly MusicEventContext _context;

        public SubscriptionRepository(MusicEventContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Subscription>> GetAllById(Guid idUsuario)
        {
            var inscricoes = await _context.Set<Subscription>()
                .Include(x => x.Evento)
                .Where(
                    x => x.IdUsuario == idUsuario && !x.Evento.Excluido
            ).ToListAsync();
            return inscricoes;
        }

        public async Task<Subscription> GetById(Guid idUsuario, Guid idEvento)
        {
            var inscricao = await _context.Set<Subscription>()
                .Where(
                    x => x.IdUsuario == idUsuario && x.IdEvento == idEvento 
            ).FirstOrDefaultAsync();
            return inscricao;
        }

    }
}
