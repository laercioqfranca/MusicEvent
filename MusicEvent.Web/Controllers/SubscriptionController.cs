using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicEvent.Core.Interfaces;
using MusicEvent.Core.Notifications;
using MusicEvent.Application.DTO;
using MusicEvent.Application.Interfaces;

namespace MusicEvent.Web.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class SubscriptionController : ApiController
    {
        private readonly ISubscriptionAppService _appService;

        public SubscriptionController(ISubscriptionAppService appService, INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _appService = appService;
        }

        [Route("GetAllById/{id}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllById(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response(id);
                }
                var response = await _appService.GetAllById(id);

                return Response(response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        //[Route("CreateSubscription")]
        //[HttpPost]
        ////[Authorize]
        //public async Task<IActionResult> Post([FromBody] InscricaoDTO inscricaoDTO)
        //{
        //    String status = null;
        //    try
        //    {
        //        var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
        //        using var connection = factory.CreateConnection();
        //        using (var channel = connection.CreateModel())
        //        {
        //            channel.QueueDeclare(
        //                queue: "newSubscription",
        //                durable: false,
        //                exclusive: false,
        //                autoDelete: false,
        //                arguments: null);

        //            string message = JsonSerializer.Serialize(inscricaoDTO);
        //            var body = Encoding.UTF8.GetBytes(message);

        //            channel.BasicPublish(
        //                exchange: "",
        //                routingKey: "newSubscription",
        //                basicProperties: null,
        //                body: body);


        //            // Fila para receber o status da operação
        //            var consumer = new EventingBasicConsumer(channel);
        //            consumer.Received += (sender, eventArgs) =>
        //            {
        //                var confirmationBody = eventArgs.Body.ToArray();
        //                var confirmationMessage = Encoding.UTF8.GetString(confirmationBody);
        //                status = confirmationMessage;
        //            };

        //            channel.BasicConsume(
        //                queue: "Log.atus",
        //                autoAck: true,
        //                consumer: consumer);
        //        }

        //        return Response(status);

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return BadRequest(e.Message);
        //    }
        //}

        [Route("CreateSubscription")]
        [HttpPost]
        public async Task<IActionResult> CreateSubscription2([FromBody] InscricaoDTO inscricaoDTO)
        {

            try
            {
                await _appService.Create(inscricaoDTO);
                return Response();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        
        
        }

            [Route("DeleteSubscription/{id}")]
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response(id);
                }

                await _appService.Delete(id);

                return Response();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
