using System;
using System.Threading.Tasks;

namespace Subscriptions.Domain.Interfaces.Infra.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
