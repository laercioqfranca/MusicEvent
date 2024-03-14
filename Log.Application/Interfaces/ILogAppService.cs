using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Log.Application.DTO;
using Log.Application.ViewModels;

namespace Log.Application.Interfaces
{
    public interface ILogAppService : IDisposable
    {
        Task CreateLog(LogViewModel logViewModel);

    }
}
