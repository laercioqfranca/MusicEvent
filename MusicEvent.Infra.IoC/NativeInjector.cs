using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MusicEvent.Infra.Bus;
using MusicEvent.Core.Interfaces;
using MusicEvent.Core.Notifications;
using MusicEvent.Infra.Data.Configuration;
using MusicEvent.Domain.Interfaces.Infra.Data;
using MusicEvent.Infra.Data.EventSourcing;
using MusicEvent.Infra.Data.Context;
using MusicEvent.Domain.Interfaces.Infra.Data.Repositories.Auth;
using MusicEvent.Infra.Data.Repositories.Auth;
using MusicEvent.Domain.Commands.Administracao;
using MusicEvent.Application.AppServices.Administracao;
using MusicEvent.Application.Interfaces.Administracao;
using MusicEvent.Application.AppServices.Auth;
using MusicEvent.Application.Interfaces.Auth;
using MusicEvent.Infra.Data.Repositories.LogRepository;
using MusicEvent.Domain.Commands.Auth;
using MusicEvent.Application.AppServices.Autenticacao;
using MusicEvent.Domain.Commands.Inscricao;
using MusicEvent.Application.Interfaces;
using MusicEvent.Application.AppServices;
using MusicEvent.Domain.Interfaces.Infra.Data.Repositories;
using MusicEvent.Infra.Data.Repositories;
using MusicEvent.Domain.Commands.Evento;

namespace MusicEvent.Infra.IoC
{
    public class NativeInjector
    {
        public static void RegisterAppServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Application
            services.AddScoped<IAutenticacaoAppService, AutenticacaoAppService>();
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<IPerfilUsuarioAppService, PerfilUsuarioAppService>();
            services.AddScoped<IEventoAppService, EventoAppService>();
            services.AddScoped<IInscricaoAppService, InscricaoAppService>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<AutenticarCommand, Unit>, AutenticacaoCommandHandler>();
            services.AddScoped<IRequestHandler<AlterarSenhaCommand, Unit>, AutenticacaoCommandHandler>();
            services.AddScoped<IRequestHandler<ResetSenhaCommand, Unit>, AutenticacaoCommandHandler>();

            services.AddScoped<IRequestHandler<UsuarioCreateCommand, Unit>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<UsuarioUpdateCommand, Unit>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<UsuarioDeleteCommand, Unit>, UsuarioCommandHandler>();

            services.AddScoped<IRequestHandler<InscricaoCreateCommand, Unit>, InscricaoCommandHandler>();
            services.AddScoped<IRequestHandler<InscricaoDeleteCommand, Unit>, InscricaoCommandHandler>();

            services.AddScoped<IRequestHandler<EventoCreateCommand, Unit>, EventoCommandHandler>();
            services.AddScoped<IRequestHandler<EventoUpdateCommand, Unit>, EventoCommandHandler>();
            services.AddScoped<IRequestHandler<EventoDeleteCommand, Unit>, EventoCommandHandler>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<ILogHistoricoRepository, LogHistoricoRepository>();

            // Infra - Data
            services.AddDbContext<MusicEventContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPerfilUsuarioRepository, PerfilUsuarioRepository>();
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IInscricaoRepository, InscricaoRepository>();

            // Infra - Service
        }
    }
}