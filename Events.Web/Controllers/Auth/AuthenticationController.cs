using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Events.Application.Interfaces.Administracao;
using Events.Application.Interfaces.Auth;
using Events.Application.ViewModels.Auth;
using Events.Core.Interfaces;
using Events.Core.JWT;
using Events.Core.Notifications;
using Events.Web.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using JwtTokenAuthentication;

namespace Events.Web.Controllers.Auth
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ApiController
    {
        private readonly IAutenticacaoAppService _appService;
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly IUsuarioAppService _usuarioAppService;

        public AuthenticationController(
            IAutenticacaoAppService appService,
            TokenConfigurations tokenConfigurations,
            SigningConfigurations signingConfigurations,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            ILogger<ApiController> logger,
            IUsuarioAppService usuarioAppService
            ) : base(notifications, mediator)
        {
            _appService = appService;
            _tokenConfigurations = tokenConfigurations;
            _signingConfigurations = signingConfigurations;
            _usuarioAppService = usuarioAppService;
        }

        #region GET

        private Token GetJwtToken(UsuarioViewModel user)
        {

            // Extraia informações do usuário e prepare as claims
            var claims = new List<Claim>
            {
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.Id), user.Id.ToString() ?? ""),
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.IdPerfil), user.IdPerfil.ToString() ?? ""),
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.Nome), user.Nome ?? ""),
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.Login), user.Login ?? ""),
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.EnumPerfil), user.Perfil.IdTipoPerfil.ToString() ?? "")
            };

            ClaimsIdentity identity = new(
             new GenericIdentity(user.Id!.ToString(), "UsuarioID"), claims);

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao + TimeSpan.FromDays(1);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtExtensions.SecurityKey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            string iss;
            switch (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
            {
                case "Development":
                default:
                    iss = "https://localhost:34637";
                    break;
            }

            var tokenOptions = new JwtSecurityToken(
                issuer: iss,
                claims: claims,
                expires: dataExpiracao,
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new()
            {
                Authenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = tokenString,
                Message = "OK"
            };
        }

        #endregion


        #region POST

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response();
                }

                var user = await _appService.Autenticar(loginViewModel);
                return user != null ? Response(GetJwtToken(user)) : Response();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        #endregion

    }
}
