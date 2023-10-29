using MusicEvent.Core.Models;
using System;

namespace MusicEvent.Domain.Models
{
    public class Evento : Entity
    {
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public Evento(string descricao, DateTime data)
        {
            Descricao = descricao;
            Data = data;
        }
    }
}
