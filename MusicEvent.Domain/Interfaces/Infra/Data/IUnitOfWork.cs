using System;
using System.Threading.Tasks;

namespace MusicEvent.Domain.Interfaces.Infra.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
