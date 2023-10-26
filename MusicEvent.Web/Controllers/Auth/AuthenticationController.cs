using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MusicEvent.Application.Interfaces.Administracao;
using MusicEvent.Application.Interfaces.Auth;
using MusicEvent.Application.ViewModels.Auth;
using MusicEvent.Core.Interfaces;
using MusicEvent.Core.JWT;
using MusicEvent.Core.Notifications;
using MusicEvent.Web.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace MusicEvent.Web.Controllers.Auth
{
    [Route("v1/authentication")]
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
            // Use a variável 'validFor' para configurar a expiração do token
            TimeSpan validFor = TimeSpan.FromDays(1);
            DateTime creationTime = DateTime.Now;
            DateTime expirationTime = creationTime + validFor;

            // Extraia informações do usuário e prepare as claims
            var claims = new List<Claim>
            {
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.Id), user.Id.ToString() ?? ""),
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.IdPerfil), user.IdPerfil.ToString() ?? ""),
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.Nome), user.Nome ?? ""),
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.Login), user.Login ?? ""),
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.RedefinirSenha), user.RedefinirSenha.ToString() ?? "")
            };

            // Crie a identidade do usuário
            ClaimsIdentity identity = new(
                new GenericIdentity(user.Id.ToString(), "UsuarioID"), claims);

            // Crie o token de segurança
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = creationTime,
                Expires = expirationTime
            });

            // Gere o token final
            var token = handler.WriteToken(securityToken);

            return new()
            {
                Authenticated = true,
                Created = creationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = expirationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK"
            };
        }

        private Token GetJwtTokenNovaSenha(UsuarioViewModel user)
        {
            TimeSpan validFor = TimeSpan.FromMinutes(10);
            DateTime creationTime = DateTime.Now;
            DateTime expirationTime = creationTime + validFor;

            var claims = new List<Claim>
            {
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.Id), user.Id.ToString() ?? ""),
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.Nome), user.Nome ?? ""),
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.Login), user.Login ?? ""),
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.NovaSenha), user.Senha.ToString() ?? "")
            };

            // Crie a identidade do usuário
            ClaimsIdentity identity = new(
                new GenericIdentity(user.Id!.ToString(), "UsuarioID"), claims);

            // Crie o token de segurança
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = creationTime,
                Expires = expirationTime
            });

            // Gere o token final
            var token = handler.WriteToken(securityToken);

            return new()
            {
                Authenticated = true,
                Created = creationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = expirationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK"
            };
        }
        #endregion


        #region POST

        [HttpPost("login")]
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


        #region PUT

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] AlterarSenhaViewModel changePasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response();
            }

            try
            {

                var commandAlterarSenha = new AlterarSenhaUsuarioViewModel
                {
                    Login = changePasswordViewModel.Login,
                    IdUsuario = changePasswordViewModel.IdUsuario,
                    SenhaNova = changePasswordViewModel.SenhaNova,
                    SenhaAtual = changePasswordViewModel.SenhaAtual
                };

                await _appService.AlterarSenha(commandAlterarSenha);

                return Response();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetSenhaViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response();
            }

            try
            {

                UsuarioViewModel usuario = new UsuarioViewModel();
                usuario.Login = resetPasswordViewModel.Login;
                usuario.Email = resetPasswordViewModel.Email;

                resetPasswordViewModel.SenhaGerada = _appService.GerarNovaSenha();
                usuario.Senha = resetPasswordViewModel.SenhaGerada;

                var token = GetJwtTokenNovaSenha(usuario);

                resetPasswordViewModel.Token = token;

                await _appService.ResetarSenhaPorEmail(resetPasswordViewModel);

                return Response();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        #endregion
    }
}
