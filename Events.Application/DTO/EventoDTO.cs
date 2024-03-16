using System;

namespace Events.Application.DTO
{
    public class EventoDTO
    {
        public Guid? Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
    }
}
