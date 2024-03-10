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
    public class InscricaoRepository : Repository<Inscricao>, IInscricaoRepository
    {
        private readonly SubscriptionsContext _context;

        public InscricaoRepository(SubscriptionsContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Inscricao>> GetAllById(Guid idUsuario)
        {
            var inscricoes = await _context.Set<Inscricao>()
                .Include(x => x.Evento)
                .Where(
                    x => x.IdUsuario == idUsuario && !x.Evento.Excluido
            ).ToListAsync();
            return inscricoes;
        }

        public async Task<Inscricao> GetById(Guid idUsuario, Guid idEvento)
        {
            var inscricao = await _context.Set<Inscricao>()
                .Where(
                    x => x.IdUsuario == idUsuario && x.IdEvento == idEvento 
            ).FirstOrDefaultAsync();
            return inscricao;
        }

    }
}
