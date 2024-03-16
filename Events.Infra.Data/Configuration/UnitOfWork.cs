using Microsoft.EntityFrameworkCore;
using Events.Domain.Interfaces.Infra.Data;
using Events.Infra.Data.Context;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Infra.Data.Configuration
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MusicEventContext _dbContext;

        public UnitOfWork(MusicEventContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }

        public async Task<bool> Commit()
        {
            int rowsAffected = await _dbContext.SaveChangesAsync();

            if(rowsAffected > 0)
            {
                var changedEntriesCopy = _dbContext.ChangeTracker.Entries().ToList();
                foreach (var entry in changedEntriesCopy)
                    entry.State = EntityState.Detached;
            }

            return rowsAffected > 0;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
