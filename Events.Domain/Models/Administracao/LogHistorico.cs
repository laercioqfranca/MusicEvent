using System;
using Events.Domain.Models;
using Events.Domain.Enum;
using Events.Core.Models;
using System.Threading.Tasks;

namespace Events.Domain.Models.Administracao
{
    public class LogHistorico
    {
        public Guid? UsuarioId { get; private set; }
        public DateTime Data { get; private set; }
        public Guid? EntidadeId { get; private set; }
        public EnumTipoLog TipoLog { get; private set; }
        public string NomeEntidade { get; private set; }
        public string Descricao { get; private set; }

        public LogHistorico() { }

        public LogHistorico(Guid? usuarioId, Guid? entidadeId, EnumTipoLog tipoLog, string nomeEntidade, string descricao)
        {
            Data = DateTime.Now;
            TipoLog = tipoLog;
            UsuarioId = usuarioId;
            Descricao = descricao;
            EntidadeId = entidadeId;
            NomeEntidade = nomeEntidade;
        }

        public LogHistorico(EnumTipoLog tipoLog, string nomeEntidade, string descricao)
        {
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
                log = new LogHistorico(null, null,
                   enumTipoLog, nomeEntidade, notificationsString);
            }
            else
            {
                log = new LogHistorico(null, null,
                    enumTipoLog, nomeEntidade, descricao);
            }
            return log;
        }

        public LogHistorico SaveLogHistorico(Guid usuarioId, Guid entidadeId, EnumTipoLog enumTipoLog, string nomeEntidade, string descricao, string notificationsString)
        {
            LogHistorico log;
            if (!string.IsNullOrEmpty(notificationsString))
            {
                log = new LogHistorico(usuarioId == null ? null : usuarioId, entidadeId == null ? null : entidadeId,
                   enumTipoLog, nomeEntidade,  notificationsString);
            }
            else
            {
                log = new LogHistorico(usuarioId == null ? null : usuarioId, entidadeId == null ? null : entidadeId,
                    enumTipoLog, nomeEntidade, descricao);
            }
            return log;
        }
    }
}
