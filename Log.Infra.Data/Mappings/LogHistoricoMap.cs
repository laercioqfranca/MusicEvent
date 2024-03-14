using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Log.Domain.Models.Administracao;


namespace Log.Infra.Data.Mappings
{
    class LogHistoricoMap : IEntityTypeConfiguration<LogHistorico>
    {
        public void Configure(EntityTypeBuilder<LogHistorico> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Data);
            builder.Property(x => x.TipoLog);

            builder.Property(x => x.UsuarioId);
            builder.Property(x => x.Descricao);
            builder.Property(x => x.EntidadeId);
            builder.Property(x => x.NomeEntidade).HasMaxLength(100);
        }
    }
}
