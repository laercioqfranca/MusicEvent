using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Subscriptions.Infra.Bus;
using Subscriptions.Core.Interfaces;
using Subscriptions.Core.Notifications;
using Subscriptions.Infra.Data.Configuration;
using Subscriptions.Domain.Interfaces.Infra.Data;
using Subscriptions.Infra.Data.EventSourcing;
using Subscriptions.Infra.Data.Context;
using Subscriptions.Domain.Interfaces.Infra.Data.Repositories.Auth;
using Subscriptions.Infra.Data.Repositories.LogRepository;
using Subscriptions.Domain.Commands.Inscricao;
using Subscriptions.Application.Interfaces;
using Subscriptions.Application.AppServices;
using Subscriptions.Domain.Interfaces.Infra.Data.Repositories;
using Subscriptions.Infra.Data.Repositories;

namespace Subscriptions.Infra.IoC
{
    public class NativeInjector
    {
        public static void RegisterAppServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Application
            services.AddScoped<ISubscriptionAppService, SubscriptionAppService>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<InscricaoCreateCommand, Unit>, InscricaoCommandHandler>();
            services.AddScoped<IRequestHandler<InscricaoDeleteCommand, Unit>, InscricaoCommandHandler>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<ILogHistoricoRepository, LogHistoricoRepository>();

            // Infra - Data
            services.AddDbContext<SubscriptionsContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IInscricaoRepository, InscricaoRepository>();

            // Infra - Service
        }
    }
}