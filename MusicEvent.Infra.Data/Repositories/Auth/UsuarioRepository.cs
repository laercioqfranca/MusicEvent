using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicEvent.Domain.Interfaces.Infra.Data.Repositories.Auth;
using MusicEvent.Domain.Models.Autenticacao;
using MusicEvent.Infra.Data.Configuration;
using MusicEvent.Infra.Data.Context;

namespace MusicEvent.Infra.Data.Repositories.Auth
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly MusicEventContext _context;

        public UsuarioRepository(MusicEventContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _context.Set<Usuario>()
                .Include(x => x.Perfil)
                .Where(x => !x.Excluido)
                .OrderByDescending(x => x.DataInclusao)
                .ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> GetByLogin(string login)
        {
            return await _context.Set<Usuario>()
                 .Include(x => x.Perfil)
                .Where(x => x.Login.ToLower() == login.ToLower()
                    && !x.Excluido
                )
                .ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> GetById(Guid id)
        {
            var usuario = await _context.Set<Usuario>()
                .Include(u => u.Perfil)
                .Where(
                    u => !u.Excluido && 
                    u.Id == id
            ).ToListAsync();
            return usuario;
        }

        public async Task<IEnumerable<Usuario>> GetByFiltro(string nome, string cpf, string email)
        {
            var usuarios =  await _context.Set<Usuario>()
                .Include(x => x.Perfil)
                .Where(
                x => !x.Excluido &&
                (nome == null || x.Nome == nome) &&
                (email == null || x.Email == email)
            )
            .ToListAsync();

            return usuarios;

        }

    }
}
