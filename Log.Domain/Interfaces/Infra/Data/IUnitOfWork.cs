using System;
using System.Threading.Tasks;

namespace Log.Domain.Interfaces.Infra.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
