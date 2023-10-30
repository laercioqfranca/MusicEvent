using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicEvent.Application.Interfaces.Auth;
using MusicEvent.Core.Interfaces;
using MusicEvent.Core.Notifications;
using MusicEvent.Domain.Enum;
using MusicEvent.Web.Configurations;

namespace MusicEvent.Web.Controllers.Administracao
{
    [Route("v1/[controller]")]
    [ApiController]
    public class PerfilUsuarioController : ApiController
    {
        private readonly IPerfilUsuarioAppService _appService;

        public PerfilUsuarioController(IPerfilUsuarioAppService appService, INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _appService = appService;
        }

        [HttpGet]
        [Route("GetAll")]
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

        [HttpGet]
        [Route("GetPerfil")]
        public async Task<IActionResult> Get()
        {
            try
            {
                Guid idPerfil = new Guid(Util.GetUserAuthenticatedData(User, ClaimAuthenticatedUser.IdPerfil));
                EnumTipoPerfil tipoPerfil = (EnumTipoPerfil)Convert.ToInt32(Util.GetUserAuthenticatedData(User, ClaimAuthenticatedUser.EnumPerfil));

                var result = await _appService.GetPerfilUsuario(idPerfil, tipoPerfil);
                return Response(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

    }
}
