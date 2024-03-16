using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Events.Domain.Models.Autenticacao;

namespace Events.Infra.Data.Mappings.Autenticacao
{
    public class PerfilUsuarioMap : IEntityTypeConfiguration<PerfilUsuario>
    {
        public void Configure(EntityTypeBuilder<PerfilUsuario> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DataInclusao);

            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(200);
            builder.Property(x => x.IdTipoPerfil);
            builder.Property(x => x.Excluido).HasDefaultValue(false);
        }
    }
}
