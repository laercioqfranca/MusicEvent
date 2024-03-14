using FluentValidation;
using Log.Domain.Commands.Inscricao;

namespace Log.Domain.Validations.Inscricao
{
    public class InscricaoCommandValidation : CommandValidation<InscricaoCreateCommand>
    {
        public InscricaoCommandValidation()
        {

            RuleFor(x => x.IdEvento)
               .NotEmpty().WithMessage("O Evento é obrigatório!");
            RuleFor(x => x.IdUsuario)
              .NotEmpty().WithMessage("O Usuário é obrigatório!");

        }
    }
}
