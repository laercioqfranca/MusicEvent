using Microsoft.EntityFrameworkCore;
using Log.Domain.Interfaces.Infra.Data;
using Log.Infra.Data.Context;
using System.Linq;
using System.Threading.Tasks;

namespace Log.Infra.Data.Configuration
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LogContext _dbContext;

        public UnitOfWork(LogContext dbContext)
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
