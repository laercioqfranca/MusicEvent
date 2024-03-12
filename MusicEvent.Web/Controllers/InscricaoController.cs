using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicEvent.Core.Interfaces;
using MusicEvent.Core.Notifications;
using MusicEvent.Application.DTO;
using MusicEvent.Application.Interfaces;
using Microsoft.AspNetCore.Connections;
using System.Text;
using RabbitMQ.Client;
using System.Text.Json;
using RabbitMQ.Client.Events;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading.Channels;

namespace MusicEvent.Web.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class InscricaoController : ApiController
    {
        private readonly IInscricaoAppService _appService;

        public InscricaoController(IInscricaoAppService appService, INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator)
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

        ////[Route("Create")]
        ////[HttpPost]
        ////[Authorize]
        ////public async Task<IActionResult> Post([FromBody] InscricaoDTO inscricaoDTO)
        ////{
        ////    try
        ////    {
        ////        if (!ModelState.IsValid)
        ////        {
        ////            NotifyModelStateErrors();
        ////            return Response(inscricaoDTO);
        ////        }

        ////        await _appService.Create(inscricaoDTO);

        ////        return Response();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Console.WriteLine(ex.InnerException.Message);
        ////        return HandleException(ex);
        ////    }
        ////}

        [Route("Create")]
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Post([FromBody] InscricaoDTO inscricaoDTO)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
                using var connection = factory.CreateConnection();
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "newSubscription",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    string message = JsonSerializer.Serialize(inscricaoDTO);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "newSubscription",
                        basicProperties: null,
                        body: body);
                }

                return Response();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Route("Delete/{id}")]
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
