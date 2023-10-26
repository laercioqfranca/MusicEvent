using FluentValidation;
using MusicEvent.Domain.Commands.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEvent.Domain.Validations.Auth
{
    public class AutenticarCommandValidation : CommandValidation<AutenticarCommand>
    {
        public AutenticarCommandValidation()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("O Login é campo obrigatório")
                .Must(BeValidCharCodes)
                .WithMessage("O Login contém charcodes bloqueados");

            RuleFor(c => c.Senha)
                .NotEmpty()
                .WithMessage("A senha é campo obrigatório")
                .Length(8, 50)
                .WithMessage("A senha deve ter entre 8 e 50 caracteres")
                .Must(BeValidCharCodes)
                .WithMessage("A senha contém caracter inválidos");
        }

        private bool BeValidCharCodes(string senha)
        {
            List<int> blockedCharCodes = new List<int> { 12644, 65440, 32 };

            foreach (char c in senha)
            {
                if (blockedCharCodes.Contains((int)c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
