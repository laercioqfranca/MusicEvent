using System;
using MusicEvent.Core.Commands;

namespace MusicEvent.Domain.Commands.Inscricao
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
