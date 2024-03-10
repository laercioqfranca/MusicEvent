﻿using Subscriptions.Core.Models;
using Subscriptions.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Subscriptions.Domain.Models.Autenticacao
{
    public class PerfilUsuario : Entity
    {
        public string Descricao { get; private set; }
        public bool Excluido { get; private set; }

        public DateTime DataInclusao { get; private set; }
        public EnumTipoPerfil IdTipoPerfil { get; private set; }

        public virtual IEnumerable<ClaimPerfil> ClaimsPerfil { get; private set; }

    }
}