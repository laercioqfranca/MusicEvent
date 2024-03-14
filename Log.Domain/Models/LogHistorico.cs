using System;
using Log.Domain.Models;
using Log.Domain.Enum;
using Log.Core.Models;
using System.Threading.Tasks;

namespace Log.Domain.Models.Administracao
{
    public class LogHistorico : Entity
    {
        public Guid? UsuarioId { get; private set; }
        public DateTime Data { get; private set; }
        public Guid? EntidadeId { get; private set; }
        public EnumTipoLog TipoLog { get; private set; }
        public string NomeEntidade { get; private set; }
        public string Descricao { get; private set; }
        public LogHistorico() { }

        public LogHistorico(Guid? usuarioId, Guid? entidadeId, EnumTipoLog tipoLog, string nomeEntidade, string descricao, DateTime data)
        {
            Id = Guid.NewGuid();
            TipoLog = tipoLog;
            UsuarioId = usuarioId;
            Descricao = descricao;
            EntidadeId = entidadeId;
            NomeEntidade = nomeEntidade;
            Data = data;

        }
    }
}
