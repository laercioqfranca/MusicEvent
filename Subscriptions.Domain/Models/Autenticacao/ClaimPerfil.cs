using System;

namespace Subscriptions.Domain.Models.Autenticacao
{
    public class ClaimPerfil
    {
        public Guid IdClaim { get; private set; }
        public Guid IdPerfil { get; private set; }

        public virtual ClaimUsuario Claim { get; private set; }
        public virtual PerfilUsuario Perfil { get; private set; }
    }
}