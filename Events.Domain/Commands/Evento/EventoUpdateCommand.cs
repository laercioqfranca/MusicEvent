using System;
using FluentValidation;
using Events.Core.Commands;
using Events.Domain.Validations.Evento;

namespace Events.Domain.Commands.Evento
{
    public class EventoUpdateCommand : Command
    {
        public EventoUpdateCommand() { }
        public Guid Id { get; protected set; }
        public string Descricao { get; protected set; }
        public DateTime Data { get; protected set; }


        public override bool IsValid()
        {
            ValidationResult = new EventoUpdateCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
