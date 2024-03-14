using System;
using MusicEvent.Core.Commands;

namespace MusicEvent.Domain.Commands.Inscricao
{
    public class SubscriptionDeleteCommand : Command
    {
        public Guid IdEvento { get; protected set; }

        public SubscriptionDeleteCommand(Guid idEvento)
        {
            IdEvento = idEvento;

        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
