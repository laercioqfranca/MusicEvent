using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Subscriptions.Infra.Data.Mappings;
using Subscriptions.Infra.Data.Mappings.Autenticacao;
using Subscriptions.Infra.Data.Mappings.LogHistoricoMap;
using System;

namespace Subscriptions.Infra.Data.Context
{
    public class SubscriptionsContext : DbContext
    {
        public SubscriptionsContext(DbContextOptions<SubscriptionsContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json").Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogHistoricoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new PerfilUsuarioMap());
            modelBuilder.ApplyConfiguration(new ClaimUsuarioMap());
            modelBuilder.ApplyConfiguration(new ClaimPerfilMap());
            modelBuilder.ApplyConfiguration(new EventoMap());
            modelBuilder.ApplyConfiguration(new InscricaoMap());


            #region Mapeamento das Tabelas de tipos e domínios

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
