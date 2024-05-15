using FluentValidation;
using MusicEvent.Domain.Commands.Inscricao;

namespace MusicEvent.Domain.Validations.Inscricao
{
    public class InscricaoCommandValidation : CommandValidation<SubscriptionCreateCommand>
    {
        public InscricaoCommandValidation()
        {
            RuleFor(x => x.IdEvento)
               .NotEmpty().WithMessage("O Evento é obrigatório!");
        }
    }
}
