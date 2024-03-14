using MusicEvent.Core.Models;
using MusicEvent.Domain.Models.Autenticacao;
using System;

namespace MusicEvent.Domain.Models
{
    public class Subscription
    {
        public Guid IdUsuario { get; set; }
        public Guid IdEvento { get; set; }
        public virtual Usuario Usuario { get; private set; }
        public virtual Evento Evento { get; private set; }

        public Subscription(Guid idUsuario, Guid idEvento)
        {
            IdUsuario = idUsuario;
            IdEvento = idEvento;
        }
    }
}
