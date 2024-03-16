using System;
using Log.Core.Commands;
using Log.Domain.Enum;
using Log.Domain.Validations;

namespace Log.Domain.Commands
{
    public class LogCreateCommand : Command
    {
        public LogCreateCommand() { }
        public Guid? UsuarioId { get; private set; }
        public Guid? EntidadeId { get; private set; }
        public EnumTipoLog TipoLog { get; private set; }
        public string NomeEntidade { get; private set; }
        public string Descricao { get; private set; }
        public DateTime Data { get; private set; }


        public override bool IsValid()
        {
            ValidationResult = new LogCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}


