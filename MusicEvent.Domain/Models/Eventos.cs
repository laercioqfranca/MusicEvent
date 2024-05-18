using MusicEvent.Core.Models;
using System;

namespace MusicEvent.Domain.Models
{
    public class Eventos : Entity
    {
        public string Descricao { get; private set; }
        public DateTime Data { get; private set; }
        public bool Excluido { get; private set; }

        public Eventos(Guid id, string descricao, DateTime data)
        {
            Id = id;
            Descricao = descricao;
            Data = data;
        }
        public void SetUpdateEvento(string descricao, DateTime data)
        {
            Descricao = descricao;
            Data = data;
        }

        public void SetExcluido(bool excluido)
        {
            Excluido = excluido;
        }

    }
}
