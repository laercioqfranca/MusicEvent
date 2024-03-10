using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Subscriptions.Application.Interfaces;
using System.Text.Json;
using Subscriptions.Application.DTO;

namespace Subscriptions.WorkerService
{
    public class Worker : BackgroundService
    {
        //private readonly IInscricaoAppService _appService;

        //public Worker(IInscricaoAppService appService)
        //{
        //    _appService = appService;
        //}
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: "newSubscription",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, eventArgs) =>
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var dto = JsonSerializer.Deserialize<InscricaoDTO>(body);

                     //_appService.Create(dto);

                    //Console.WriteLine(message?.ToString());
                };

                channel.BasicConsume(
                    queue: "newSubscription",
                    autoAck: true,
                    consumer: consumer);
                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
