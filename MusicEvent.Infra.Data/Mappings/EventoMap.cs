﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MusicEvent.Domain.Models;

namespace MusicEvent.Infra.Data.Mappings
{
    public class EventoMap : IEntityTypeConfiguration<Eventos>
    {
        public void Configure(EntityTypeBuilder<Eventos> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(64);
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.Excluido).HasDefaultValue(false);
        }
    }
}
