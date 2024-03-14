using Log.Core.Models;
using Log.Domain.Utils;
using System;
using System.Collections.Generic;

namespace Log.Domain.Models
{
    public class Usuario : Entity
    {
        public string Nome { get; private set; }
        public int Idade { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Salt { get; private set; }
        public string Email { get; private set; }
        public Guid? IdPerfil { get; private set; }
        public DateTime DataInclusao { get; private set; }
        public bool Excluido { get; private set; }

        public virtual IEnumerable<Inscricao> EventoUsuarios { get; set; }
    }
}