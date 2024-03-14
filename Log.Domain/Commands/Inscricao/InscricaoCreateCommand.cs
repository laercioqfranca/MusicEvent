using System;
using Log.Core.Commands;
using Log.Domain.Validations.Inscricao;

namespace Log.Domain.Commands.Inscricao
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
