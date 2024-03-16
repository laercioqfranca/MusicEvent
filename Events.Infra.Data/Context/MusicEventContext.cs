﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Events.Infra.Data.Mappings;
using Events.Infra.Data.Mappings.Autenticacao;
using System;

namespace Events.Infra.Data.Context
{
    public class MusicEventContext : DbContext
    {
        public MusicEventContext(DbContextOptions<MusicEventContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json").Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new PerfilUsuarioMap());
            modelBuilder.ApplyConfiguration(new ClaimUsuarioMap());
            modelBuilder.ApplyConfiguration(new ClaimPerfilMap());
            modelBuilder.ApplyConfiguration(new EventoMap());
            modelBuilder.ApplyConfiguration(new SubscriptionMap());


            #region Mapeamento das Tabelas de tipos e domínios

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
