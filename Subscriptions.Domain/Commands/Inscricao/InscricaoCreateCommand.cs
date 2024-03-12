using System;
using Subscriptions.Core.Commands;
using Subscriptions.Domain.Validations.Inscricao;

namespace Subscriptions.Domain.Commands.Inscricao
{
    public class InscricaoCreateCommand : Command
    {
        public InscricaoCreateCommand() { }
        public Guid IdEvento { get; protected set; }
        public Guid IdUsuario { get; protected set; }


        public override bool IsValid()
        {
            ValidationResult = new InscricaoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
