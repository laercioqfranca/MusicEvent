using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MusicEvent.Domain.Commands.Administracao;

namespace MusicEvent.Domain.Validations.Administracao
{
    public class UsuarioCreateCommandValidation : CommandValidation<UsuarioCreateCommand>
    {
        public UsuarioCreateCommandValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O Nome do usuário é obrigatório!");

            RuleFor(x => x.Idade)
               .NotEmpty().WithMessage("A Idade do usuário é obrigatória!");
            
            RuleFor(x => x.Idade > 18)
              .NotEmpty().WithMessage("A idade precisa ser maior ou igual a 18 anos!");

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("O E-mail do usuário é obrigatório!");

            RuleFor(x => x.Senha)
               .NotEmpty().WithMessage("A Senha é obrigatória!");

        }
    }
}
