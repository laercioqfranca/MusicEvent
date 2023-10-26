using MusicEvent.Application.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEvent.Application.Interfaces.Auth
{
    public interface IAutenticacaoAppService : IDisposable
    {
        Task AlterarSenha(AlterarSenhaUsuarioViewModel commandAlterarSenha);
        Task<UsuarioViewModel> Autenticar(LoginViewModel loginViewModel);
        Task ResetarSenhaPorEmail(ResetSenhaViewModel resetPasswordViewModel);
        string GerarNovaSenha();
    }
}
