using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subscriptions.Core.Commands;

namespace Subscriptions.Domain.Commands.Administracao
{
    public class UsuarioDeleteCommand : Command
    {
        public Guid IdUsuario { get; protected set; }

        public UsuarioDeleteCommand(Guid idUsuario)
        {
            IdUsuario = idUsuario;

        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
