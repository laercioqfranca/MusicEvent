using FluentValidation;
using MusicEvent.Domain.Commands.Inscricao;

namespace MusicEvent.Domain.Validations.Inscricao
{
    public class InscricaoCommandValidation : CommandValidation<InscricaoCreateCommand>
    {
        public InscricaoCommandValidation()
        {

            RuleFor(x => x.IdEvento)
               .NotEmpty().WithMessage("A IdEvento é obrigatória!");

        }
    }
}
