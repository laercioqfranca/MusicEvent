using MusicEvent.Core.Commands;
using MusicEvent.Core.JWT;
using MusicEvent.Domain.Validations.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEvent.Domain.Commands.Auth
{
    public class ResetSenhaCommand : Command
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string Url { get; set; }
        public string SenhaGerada { get; set; }
        public Token Token { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new ResetSenhaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
