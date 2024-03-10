﻿using Subscriptions.Core.Models;
using System;

namespace Subscriptions.Domain.Models.Autenticacao
{
    public class ClaimUsuario : Entity
    {
        public string Descricao { get; private set; }
        public DateTime DataInclusao { get; private set; }
        public bool Excluido { get; private set; }
    }
}