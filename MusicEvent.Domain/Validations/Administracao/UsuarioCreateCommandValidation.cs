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
               .NotEmpty().WithMessage("A Idade do usuário é obrigatório!");

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("O E-mail do usuário é obrigatório!");

            RuleFor(x => x.IdPerfil)
               .NotEmpty().WithMessage("O Grupo de Acesso é obrigatório!");

        }
    }
}
