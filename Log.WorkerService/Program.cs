using MediatR;
using Log.Application.AutoMapper;
using Log.Infra.IoC;
using Log.WorkerService;
using MassTransit;
using Log.Application.ViewModels;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddMediatR(typeof(NativeInjector));
NativeInjector.RegisterAppServices(builder.Services);

var configurarion = builder.Configuration;
var conexao = configurarion.GetSection("MassTransitAzure")["Conexao"] ?? string.Empty;
var fila = configurarion.GetSection("MassTransitAzure")["NomeFila"] ?? string.Empty;

builder.Services.AddMassTransit(x =>
{
    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(conexao);

        cfg.ReceiveEndpoint("log", e =>
        {
            e.PrefetchCount = 100;
            e.Consumer<LogConsumer>();
        });


    });

});
IHost host = builder.Build();

IConfiguration Config = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", false, true)
.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", false, true).Build();

host.Run();

