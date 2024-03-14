using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Log.Infra.Bus;
using Log.Core.Interfaces;
using Log.Core.Notifications;
using Log.Infra.Data.Configuration;
using Log.Domain.Interfaces.Infra.Data;
using Log.Infra.Data.EventSourcing;
using Log.Infra.Data.Context;
using Log.Infra.Data.Repositories.LogRepository;
using Log.Application.Interfaces;
using Log.Application.AppServices;
using Log.Domain.Interfaces.Infra.Data.Repositories;
using Log.Infra.Data.Repositories;
using Log.Domain.Commands;

namespace Log.Infra.IoC
{
    public class NativeInjector
    {
        public static void RegisterAppServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Application
            services.AddScoped<ILogAppService, LogAppService>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<LogCreateCommand, Unit>, LogCommandHandler>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<ILogHistoricoRepository, LogHistoricoRepository>();

            // Infra - Data
            services.AddDbContext<LogContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Infra - Service
        }
    }
}