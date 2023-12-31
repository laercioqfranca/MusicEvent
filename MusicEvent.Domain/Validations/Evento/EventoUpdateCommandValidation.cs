﻿using FluentValidation;
using MusicEvent.Domain.Commands.Evento;

namespace MusicEvent.Domain.Validations.Evento
{
    public class EventoUpdateCommandValidation : CommandValidation<EventoUpdateCommand>
    {
        public EventoUpdateCommandValidation()
        {

            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("O Id é obrigatório!");

            RuleFor(x => x.Descricao)
               .NotEmpty().WithMessage("A Descricao é obrigatória!");

            RuleFor(x => x.Data)
               .NotEmpty().WithMessage("A Data é obrigatória!");

        }
    }
}
