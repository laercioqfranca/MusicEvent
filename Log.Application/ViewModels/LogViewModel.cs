using Log.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Application.ViewModels
{
    public class LogViewModel
    {
        public Guid? UsuarioId { get; set; }
        public Guid? EntidadeId { get; set; }
        public DateTime Data { get; set; }
        public EnumTipoLog TipoLog { get; set; }
        public string NomeEntidade { get; set; }
        public string Descricao { get; set; }
    }
}
