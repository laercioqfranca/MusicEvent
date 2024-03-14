using MediatR;
using Log.Application.AutoMapper;
using Log.Infra.IoC;
using Log.WorkerService;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddMediatR(typeof(NativeInjector));
NativeInjector.RegisterAppServices(builder.Services);

IHost host = builder.Build();

IConfiguration Config = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", false, true)
.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", false, true).Build();

host.Run();

