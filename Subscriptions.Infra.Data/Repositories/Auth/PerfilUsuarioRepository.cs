using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Subscriptions.Domain.Enum;
using Subscriptions.Domain.Interfaces.Infra.Data.Repositories.Auth;
using Subscriptions.Domain.Models.Autenticacao;
using Subscriptions.Infra.Data.Configuration;
using Subscriptions.Infra.Data.Context;

namespace Subscriptions.Infra.Data.Repositories.Auth
{
    public class PerfilUsuarioRepository : Repository<PerfilUsuario>, IPerfilUsuarioRepository
    {
        public PerfilUsuarioRepository(SubscriptionsContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<PerfilUsuario>> GetPerfilUsuario(EnumTipoPerfil? tipoPerfil)
        {
            IQueryable<PerfilUsuario> query = DbSet.Where(p =>
                                            (!p.Excluido) &&
                                                ((tipoPerfil == EnumTipoPerfil.Administrador ) ||
                                                    (tipoPerfil == EnumTipoPerfil.Cliente))
                                                ).OrderBy(x => x.Descricao);
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PerfilUsuario>> GetAll()
        {
            IQueryable<PerfilUsuario> query = DbSet.Where(p => (!p.Excluido)).OrderBy(x => x.Descricao);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}
