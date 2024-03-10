using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subscriptions.Core.Commands;
using Subscriptions.Domain.Validations.Auth;

namespace Subscriptions.Domain.Commands.Auth
{
    public class AutenticarCommand : Command
    {
        public string Login { get; protected set; }
        public string Senha { get; protected set; }

        public AutenticarCommand(string login, string senha)
        {
            Login = login;
            Senha = senha;
        }

        public override bool IsValid()
        {
            ValidationResult = new AutenticarCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
