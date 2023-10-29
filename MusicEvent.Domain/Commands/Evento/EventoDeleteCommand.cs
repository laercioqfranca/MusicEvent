using System;
using MusicEvent.Core.Commands;

namespace MusicEvent.Domain.Commands.Evento
{
    public class EventoDeleteCommand : Command
    {
        public Guid IdEvento { get; protected set; }

        public EventoDeleteCommand(Guid idEvento)
        {
            IdEvento = idEvento;

        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
