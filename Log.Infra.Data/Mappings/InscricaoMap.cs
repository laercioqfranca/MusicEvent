using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Log.Domain.Models;

namespace Log.Infra.Data.Mappings
{
    public class InscricaoMap : IEntityTypeConfiguration<Inscricao>
    {
        public void Configure(EntityTypeBuilder<Inscricao> builder)
        {
            builder.HasKey(x => new { x.IdUsuario, x.IdEvento });

            builder.HasOne(x => x.Evento).WithMany().HasForeignKey(x => x.IdEvento).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            builder.HasOne(x => x.Usuario).WithMany(y => y.EventoUsuarios).HasForeignKey(x => x.IdUsuario).Metadata.DeleteBehavior = DeleteBehavior.Restrict;

        }
    }
}
