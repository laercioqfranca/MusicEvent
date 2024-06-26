﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MusicEvent.Domain.Models;

namespace MusicEvent.Infra.Data.Mappings
{
    public class SubscriptionMap : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(x => new { x.IdUsuario, x.IdEvento });

            builder.HasOne(x => x.Evento).WithMany().HasForeignKey(x => x.IdEvento).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            builder.HasOne(x => x.Usuario).WithMany(y => y.EventoUsuarios).HasForeignKey(x => x.IdUsuario).Metadata.DeleteBehavior = DeleteBehavior.Restrict;

        }
    }
}
