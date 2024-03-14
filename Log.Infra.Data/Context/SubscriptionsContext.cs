﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Log.Infra.Data.Mappings;
using Log.Infra.Data.Mappings.LogHistoricoMap;
using System;

namespace Log.Infra.Data.Context
{
    public class LogContext : DbContext
    {
        public LogContext(DbContextOptions<LogContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json").Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogHistoricoMap());
            modelBuilder.ApplyConfiguration(new InscricaoMap());


            #region Mapeamento das Tabelas de tipos e domínios

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}