using Subscriptions.Application.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriptions.Application.Interfaces.Auth
{
    public interface IAutenticacaoAppService : IDisposable
    {
        Task<UsuarioViewModel> Autenticar(LoginViewModel loginViewModel);
    
    }
}
