using FluentValidation;
using MusicEvent.Domain.Commands.Auth;
using System.Collections.Generic;

namespace MusicEvent.Domain.Validations.Auth
{
    public class ResetSenhaCommandValidation : CommandValidation<ResetSenhaCommand>
    {
        public ResetSenhaCommandValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O E-mail é obrigatório")
                .Must(BeValidCharCodes)
                .WithMessage("O campo contém caracter inválidos");

            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("O Login é obrigatório")
                .Must(BeValidCharCodes)
                .WithMessage("A senha contém charcodes bloqueados");
        }

        private bool BeValidCharCodes(string senha)
        {
            //valida caracteres invisiveis e espaço em branco
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
