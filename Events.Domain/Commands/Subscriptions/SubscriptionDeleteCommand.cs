using System;
using Events.Core.Commands;

namespace Events.Domain.Commands.Inscricao
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
