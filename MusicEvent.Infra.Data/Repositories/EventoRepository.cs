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
    public class EventoRepository : Repository<Eventos>, IEventoRepository
    {
        private readonly MusicEventContext _context;
        public EventoRepository(MusicEventContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Eventos>> GetAll()
        {
            IQueryable<Eventos> query = DbSet.Where(x => !x.Excluido).OrderBy(x => x.Data);
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<Eventos> GetById(Guid idEvento)
        {
            var evento = await _context.Set<Eventos>()
                .Where(
                    x => x.Id == idEvento && !x.Excluido
            ).FirstOrDefaultAsync();
            return evento;
        }
    }
}
