using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Core.Commands;
using Events.Domain.Validations.Administracao;

namespace Events.Domain.Commands.Administracao
{
    public class UsuarioCreateCommand : Command
    {
        public UsuarioCreateCommand() { }

        public string Nome { get; protected set; }
        public int Idade { get; protected set; }
        public string Email { get; protected set; }
        public string Senha { get; protected set; }

        public Guid? IdPerfil { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new UsuarioCreateCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
