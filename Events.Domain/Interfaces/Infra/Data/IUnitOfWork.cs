using System;
using System.Threading.Tasks;

namespace Events.Domain.Interfaces.Infra.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
