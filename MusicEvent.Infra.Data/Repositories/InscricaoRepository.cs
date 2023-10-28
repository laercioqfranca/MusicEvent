using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicEvent.Domain.Interfaces.Infra.Data.Repositories;
using MusicEvent.Domain.Models;
using MusicEvent.Infra.Data.Configuration;
using MusicEvent.Infra.Data.Context;

namespace MusicEvent.Infra.Data.Repositories
{
    public class InscricaoRepository : Repository<Inscricao>, IInscricaoRepository
    {
        private readonly MusicEventContext _context;

        public InscricaoRepository(MusicEventContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Inscricao>> GetAllById(Guid idUsuario)
        {
            var inscricoes = await _context.Set<Inscricao>()
                .Include(x => x.Evento)
                .Where(
                    x => x.IdUsuario == idUsuario
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
