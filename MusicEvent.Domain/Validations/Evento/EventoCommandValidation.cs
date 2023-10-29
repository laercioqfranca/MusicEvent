using FluentValidation;
using MusicEvent.Domain.Commands.Evento;

namespace MusicEvent.Domain.Validations.Evento
{
    public class EventoCommandValidation : CommandValidation<EventoCreateCommand>
    {
        public EventoCommandValidation()
        {

            RuleFor(x => x.Descricao)
               .NotEmpty().WithMessage("A Descricao é obrigatória!");

            RuleFor(x => x.Data)
               .NotEmpty().WithMessage("A Data é obrigatória!");

        }
    }
}
