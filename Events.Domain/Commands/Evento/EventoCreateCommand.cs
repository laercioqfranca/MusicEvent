using System;
using Events.Core.Commands;
using Events.Domain.Validations.Evento;

namespace Events.Domain.Commands.Evento
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
