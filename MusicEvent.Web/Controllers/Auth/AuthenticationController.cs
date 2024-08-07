﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MusicEvent.Application.Interfaces.Administracao;
using MusicEvent.Application.Interfaces.Auth;
using MusicEvent.Application.ViewModels.Auth;
using MusicEvent.Core.Interfaces;
using MusicEvent.Core.JWT;
using MusicEvent.Core.Notifications;
using MusicEvent.Web.Configurations;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

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
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.EnumPerfil), user.Perfil.IdTipoPerfil.ToString() ?? "")
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

        #endregion


        #region POST

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginEncryptViewModel loginEncrypt)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response();
                }

                string decrypted = DecryptString(loginEncrypt.encrypted);
                var loginViewModel = JsonConvert.DeserializeObject<LoginViewModel>(decrypted);

                var user = await _appService.Autenticar(loginViewModel);
                return user != null ? Response(GetJwtToken(user)) : Response();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private string DecryptString(string cipherText)
        {
            var key = Encoding.UTF8.GetBytes("sJgppTL83sr1HILcta0LX0yEMJUo8qs3"); // replace with your secret key
            var iv = Encoding.UTF8.GetBytes("HR$2pIjHR$2pIj12"); // replace with your initialization vector

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var memoryStream = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        #endregion

    }
}
