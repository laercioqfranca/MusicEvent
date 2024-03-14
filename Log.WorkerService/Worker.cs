using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Log.Application.Interfaces;
using System.Text.Json;
using Log.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Log.WorkerService
{
    public class Worker : BackgroundService
    {
        public IServiceScopeFactory _serviceScopeFactory;
        public Worker(IServiceScopeFactory serviceScopeFactory, IServiceProvider services)
        {
            _serviceScopeFactory = serviceScopeFactory;
            Services = services;
        }

        public IServiceProvider Services { get; }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            String status = "Inscrição processada com sucesso!";
            while (!stoppingToken.IsCancellationRequested)
            {
    
                var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                
                // Fila de entrada das inscrições
                channel.QueueDeclare(queue: "log",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                // Fila para devolver o status do cadastro
                channel.QueueDeclare(
                queue: "Log.atus",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                
                consumer.Received += async (sender, eventArgs) =>
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var dto = JsonSerializer.Deserialize<InscricaoDTO>(body);

                    using (var scope = Services.CreateScope())
                    {
                        var scoped = scope.ServiceProvider.GetRequiredService<ILogAppService>();
                        try
                        {

                            using (var connection = factory.CreateConnection())
                            {
                                await scoped.Create(dto);
                            }
                        }
                        catch (Exception ex)
                        {
                            status = ex.Message;
                        }
                    }

                };

                var statusBody = Encoding.UTF8.GetBytes(status);
                channel.BasicPublish(
                    exchange: "",
                    routingKey: "Log.atus",
                    basicProperties: null,
                    body: statusBody);

                channel.BasicConsume(
                    queue: "log",
                    autoAck: true,
                    consumer: consumer);

                // Tempo de espera para verifica novamente a fila
                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
