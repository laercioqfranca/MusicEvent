using MusicEvent.Core.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEvent.Application.ViewModels.Auth
{
    public class ResetSenhaViewModel
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string Url { get; set; }
        public Token Token { get; set; }
        public string SenhaGerada { get; set; }

        public ResetSenhaViewModel() { }

    }
}
