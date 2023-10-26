using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEvent.Application.ViewModels.Auth
{
    public class AlterarSenhaUsuarioViewModel
    {
        public string Login { get; set; }
        public string SenhaNova { get; set; }
        public string SenhaAtual { get; set; }
        public Guid IdUsuario { get; set; }
    }
}
