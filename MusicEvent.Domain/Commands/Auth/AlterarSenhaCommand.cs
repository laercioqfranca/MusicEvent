using MusicEvent.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEvent.Domain.Commands.Auth
{
    public class AlterarSenhaCommand : Command
    {

        public string Login { get; protected set; }
        public string SenhaAtual { get; protected set; }
        public string SenhaNova { get; protected set; }
        public Guid IdUsuario { get; protected set; }

        public AlterarSenhaCommand(Guid idusuario, string senhaatual, string senhanova, string login)
        {
            Login = login;
            SenhaAtual = senhaatual;
            SenhaNova = senhanova;
            IdUsuario = idusuario;
        }


        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(SenhaAtual) && !string.IsNullOrEmpty(SenhaNova);
        }
    }
}
