using Events.Core.Models;
using Events.Domain.Models.Autenticacao;
using System;

namespace Events.Domain.Models
{
    public class Subscription
    {
        public Guid IdUsuario { get; set; }
        public Guid IdEvento { get; set; }
        public virtual Usuario Usuario { get; private set; }
        public virtual Eventos Evento { get; private set; }

        public Subscription(Guid idUsuario, Guid idEvento)
        {
            IdUsuario = idUsuario;
            IdEvento = idEvento;
        }
    }
}
