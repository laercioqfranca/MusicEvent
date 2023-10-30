using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEvent.Application.ViewModels.Auth
{
    public class UsuarioViewModel
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }

        public string Senha { get; set; }
        public string Salt { get; set; }
        public DateTime DataInclusao { get; set; }

        public bool Excluido { get; set; }

        public UsuarioViewModel() { }

        public Guid? IdPerfil { get; set; }
        public PerfilUsuarioViewModel Perfil { get; set; }
        public IEnumerable<string> Claims { get; set; }

    }
}
