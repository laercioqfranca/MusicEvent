using System;
using Events.Core.Commands;
using Events.Domain.Validations.Inscricao;

namespace Events.Domain.Commands.Inscricao
{
    public class SubscriptionCreateCommand : Command
    {
        public SubscriptionCreateCommand() { }
        public Guid IdEvento { get; protected set; }


        public override bool IsValid()
        {
            ValidationResult = new InscricaoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
