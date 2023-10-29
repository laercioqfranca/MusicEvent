using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicEvent.Application.Interfaces;
using MusicEvent.Core.Interfaces;
using MusicEvent.Core.Notifications;

namespace MusicEvent.Web.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class EventoController : ApiController
    {
        private readonly IEventoAppService _appService;

        public EventoController(IEventoAppService appService, INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _appService = appService;
        }

        [HttpGet]
        [Route("GetAll")]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _appService.GetAll();
                return Response(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

    }
}
