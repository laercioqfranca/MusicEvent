using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Application.DTO;
using Events.Application.Interfaces;
using Events.Core.Interfaces;
using Events.Core.Notifications;

namespace Events.Web.Controllers
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
        [Authorize]
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

        [Route("GetById/{id}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response(id);
                }
                var response = await _appService.GetById(id);

                return Response(response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [Route("Create")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] EventoDTO eventoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response(eventoDTO);
                }

                await _appService.Create(eventoDTO);

                return Response();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [Route("Update")]
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] EventoDTO eventoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response(eventoDTO);
                }

                await _appService.Update(eventoDTO);

                return Response();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
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
