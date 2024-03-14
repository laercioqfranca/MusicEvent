using System;
using Log.Core.Commands;

namespace Log.Domain.Commands.Inscricao
{
    public class InscricaoDeleteCommand : Command
    {
        public Guid IdEvento { get; protected set; }

        public InscricaoDeleteCommand(Guid idEvento)
        {
            IdEvento = idEvento;

        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
