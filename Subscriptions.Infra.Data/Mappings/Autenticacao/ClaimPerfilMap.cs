using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscriptions.Domain.Models.Autenticacao;

namespace Subscriptions.Infra.Data.Mappings.Autenticacao
{
    public class ClaimPerfilMap : IEntityTypeConfiguration<ClaimPerfil>
    {
        public void Configure(EntityTypeBuilder<ClaimPerfil> builder)
        {
            builder.HasKey(x => new { x.IdClaim, x.IdPerfil });

            builder.HasOne(x => x.Claim).WithMany().HasForeignKey(x => x.IdClaim).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            builder.HasOne(x => x.Perfil).WithMany(y => y.ClaimsPerfil).HasForeignKey(x => x.IdPerfil).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}