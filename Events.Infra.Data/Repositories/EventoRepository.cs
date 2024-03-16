using Microsoft.EntityFrameworkCore;
using Events.Domain.Interfaces.Infra.Data.Repositories;
using Events.Domain.Models;
using Events.Infra.Data.Configuration;
using Events.Infra.Data.Context;

namespace Events.Infra.Data.Repositories
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
            IQueryable<Eventos> query = DbSet.Where(x => !x.Excluido).OrderByDescending(x => x.Data);
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
