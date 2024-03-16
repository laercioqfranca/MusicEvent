using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.DTO
{
    public class UsuarioDTO
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Guid? IdPerfil { get; set; }

    }
}
