using System;
using FluentValidation;
using MusicEvent.Core.Commands;
using MusicEvent.Domain.Validations.Evento;

namespace MusicEvent.Domain.Commands.Evento
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
