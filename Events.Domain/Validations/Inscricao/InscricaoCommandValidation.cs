using FluentValidation;
using Events.Domain.Commands.Inscricao;

namespace Events.Domain.Validations.Inscricao
{
    public class InscricaoCommandValidation : CommandValidation<SubscriptionCreateCommand>
    {
        public InscricaoCommandValidation()
        {

            RuleFor(x => x.IdEvento)
               .NotEmpty().WithMessage("A IdEvento é obrigatória!");

        }
    }
}
