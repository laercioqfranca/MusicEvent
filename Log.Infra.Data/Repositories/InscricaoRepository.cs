using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Log.Domain.Interfaces.Infra.Data.Repositories;
using Log.Domain.Models;
using Log.Infra.Data.Configuration;
using Log.Infra.Data.Context;

namespace Log.Infra.Data.Repositories
{
    public class InscricaoRepository : Repository<Inscricao>, IInscricaoRepository
    {
        private readonly LogContext _context;

        public InscricaoRepository(LogContext dbContext) : base(dbContext)
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
