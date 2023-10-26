using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEvent.Application.ViewModels.Auth
{
    public class AlterarSenhaViewModel
    {
        public Guid IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo Login é obrigatório!")]
        public string Login { get; set; }

        [StringLength(50, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 50 caracteres")]
        [Required(ErrorMessage = "A nova senha não foi informada!")]
        public string SenhaNova { get; set; }

        [Required(ErrorMessage = "A senha atual não foi informada!")]
        public string SenhaAtual { get; set; }
    }
}
