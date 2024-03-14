using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Log.Application.DTO;

namespace Log.Application.Interfaces
{
    public interface ILogAppService : IDisposable
    {
        Task Create(InscricaoDTO inscricaoDTO);

    }
}
