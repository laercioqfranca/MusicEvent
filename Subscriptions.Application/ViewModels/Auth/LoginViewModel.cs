using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriptions.Application.ViewModels.Auth
{
    public class LoginViewModel
    {
        [StringLength(50, ErrorMessage = "O nome de usuário possui um limite máximo de 50 caracteres")]
        [Required(ErrorMessage = "O nome de usuário não foi informado!")]
        public string Login { get; set; }

        [StringLength(50, ErrorMessage = "A senha de usuário possui um limite máximo de 50 caracteres")]
        [Required(ErrorMessage = "A senha não foi informada!")]
        public string Senha { get; set; }
    }
}
