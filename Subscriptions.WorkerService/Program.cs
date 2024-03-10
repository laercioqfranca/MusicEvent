using MediatR;
using Subscriptions.Application.AutoMapper;
using Subscriptions.Infra.IoC;
using Subscriptions.WorkerService;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddMediatR(typeof(NativeInjector));
NativeInjector.RegisterAppServices(builder.Services);

IHost host = builder.Build();

host.Run();


//using Subscriptions.WorkerService;

//IHost host = Host.CreateDefaultBuilder(args)
//    .ConfigureServices(services =>
//    {
//        services.AddHostedService<Worker>();
//    })
//    .Build();

//host.Run();
