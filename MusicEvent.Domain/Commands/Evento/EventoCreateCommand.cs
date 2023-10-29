using System;
using MusicEvent.Core.Commands;
using MusicEvent.Domain.Validations.Evento;

namespace MusicEvent.Domain.Commands.Evento
{
    public class EventoCreateCommand : Command
    {
        public EventoCreateCommand() { }
        public string Descricao { get; protected set; }
        public DateTime Data { get; protected set; }


        public override bool IsValid()
        {
            ValidationResult = new EventoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
