﻿using FluentValidation;
using Subscriptions.Domain.Commands.Inscricao;

namespace Subscriptions.Domain.Validations.Inscricao
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
