using System;
using Subscriptions.Core.Commands;
using Subscriptions.Domain.Validations.Evento;

namespace Subscriptions.Domain.Commands.Evento
{
    public class EventoCreateCommand : Command
    {
        public EventoCreateCommand() { }
        public string Descricao { get; protected set; }
        public DateTime Data { get; protected set; }


        public override bool IsValid()
        {
            ValidationResult = new EventoCreateCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
