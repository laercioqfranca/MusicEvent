using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Events.Domain.Enum;
using Events.Domain.Interfaces.Infra.Data.Repositories.Auth;
using Events.Domain.Models.Autenticacao;
using Events.Infra.Data.Configuration;
using Events.Infra.Data.Context;

namespace Events.Infra.Data.Repositories.Auth
{
    public class PerfilUsuarioRepository : Repository<PerfilUsuario>, IPerfilUsuarioRepository
    {
        public PerfilUsuarioRepository(MusicEventContext dbContext) : base(dbContext)
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
