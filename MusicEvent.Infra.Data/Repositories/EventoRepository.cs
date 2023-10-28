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
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        public EventoRepository(MusicEventContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Evento>> GetAll()
        {
            IQueryable<Evento> query = DbSet.OrderBy(x => x.Data);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}
