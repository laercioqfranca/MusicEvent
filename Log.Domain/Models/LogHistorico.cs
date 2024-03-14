using System;
using Log.Domain.Models;
using Log.Domain.Enum;
using Log.Core.Models;
using System.Threading.Tasks;

namespace Log.Domain.Models.Administracao
{
    public class LogHistorico : Entity
    {
        public Guid UsuarioId { get; private set; }
        public DateTime Data { get; private set; }
        public Guid EntidadeId { get; private set; }
        public EnumTipoLog TipoLog { get; private set; }
        public string NomeEntidade { get; private set; }
        public string Descricao { get; private set; }
        public LogHistorico() { }

        public LogHistorico(Guid usuarioId, Guid entidadeId, EnumTipoLog tipoLog, string nomeEntidade, string descricao)
        {
            Id = Guid.NewGuid();
            Data = DateTime.Now;
            TipoLog = tipoLog;
            UsuarioId = usuarioId;
            Descricao = descricao;
            EntidadeId = entidadeId;
            NomeEntidade = nomeEntidade;

        }

        public LogHistorico(EnumTipoLog tipoLog, string nomeEntidade, string descricao)
        {
            Id = Guid.NewGuid();
            Data = DateTime.Now;
            TipoLog = tipoLog;            
            Descricao = descricao;
            NomeEntidade = nomeEntidade;

        }

        public LogHistorico SaveLogHistorico(EnumTipoLog enumTipoLog, string nomeEntidade, string descricao, string notificationsString)
        {
            LogHistorico log;
            if (!string.IsNullOrEmpty(notificationsString))
            {
                log = new LogHistorico(new Guid(), new Guid(),
                   enumTipoLog, nomeEntidade, notificationsString);
            }
            else
            {
                log = new LogHistorico(new Guid(), new Guid(),
                    enumTipoLog, nomeEntidade, descricao);
            }
            return log;
        }

        public LogHistorico SaveLogHistorico(Guid usuarioId, Guid entidadeId, EnumTipoLog enumTipoLog, string nomeEntidade, string descricao, string notificationsString)
        {
            LogHistorico log;
            if (!string.IsNullOrEmpty(notificationsString))
            {
                log = new LogHistorico(usuarioId == null ? new Guid() : usuarioId, entidadeId == null ? new Guid() : entidadeId,
                   enumTipoLog, nomeEntidade,  notificationsString);
            }
            else
            {
                log = new LogHistorico(usuarioId == null ? new Guid() : usuarioId, entidadeId == null ? new Guid() : entidadeId,
                    enumTipoLog, nomeEntidade, descricao);
            }
            return log;
        }
    }
}
