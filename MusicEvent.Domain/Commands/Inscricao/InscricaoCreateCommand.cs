using System;
using MusicEvent.Core.Commands;
using MusicEvent.Domain.Validations.Inscricao;

namespace MusicEvent.Domain.Commands.Inscricao
{
    public class InscricaoCreateCommand : Command
    {
        public InscricaoCreateCommand() { }
        public Guid IdEvento { get; protected set; }


        public override bool IsValid()
        {
            ValidationResult = new InscricaoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
