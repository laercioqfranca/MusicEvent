using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace MusicEvent.Web.Configurations
{
    internal class Util
    {
        internal const string ClaimTipoPerfil = "EnumPerfil";
        internal const string ClaimAdministrador = "1";
        internal const string ClaimCliente = "2";



        internal const string ClaimNamePermissao = "Permission";
        /* ------------------ Instruções ------------------
         * Os serviços RESTful que implementarem requisitos de autorização deverão implementar os seguintes atributos:
         * - [Authorize]: para autenticação do usuário via token JWT
         * - [ClaimRequirement(Util.PermissaoClaimName, XXX)]: para autorização de uma funcionalidade específica
         * A variável XXX deve ser definida conforme as constantes abaixo, cadastradas na tabela ClaimUsuario
         * 
         * Conforme definido no caso de uso, caso o perfil (PerfilUsuario) não esteja associado à claim (ClaimUsuario),
         * o desenvolvedor deverá associar o perfil ao claim através da inclusão via script de registro na tabela ClaimPerfil,
         * a qual é composta pelo id da claim (ClaimUsuario) e o id do perfil (PerfilUsuario)         
        */

        internal static string GetUserAuthenticatedData(ClaimsPrincipal user, ClaimAuthenticatedUser claim)
        {
            string result = null;
            if (user != null && user.Identity.IsAuthenticated)
            {
                string claimName = GetEnumDescription(claim);
                Claim claimValue = user.Claims.Where(c => c.Type == claimName).FirstOrDefault();
                if (claimValue != null)
                    result = claimValue.Value;
            }
            return result;
        }

        internal static List<Guid> GetUserAuthenticatedDataList(ClaimsPrincipal user, ClaimAuthenticatedUser claim)
        {
            List<Guid> result = new List<Guid>();
            if (user != null && user.Identity.IsAuthenticated)
            {
                string claimName = GetEnumDescription(claim);
                List<Claim> claimValue = user.Claims.Where(c => c.Type == claimName).ToList();
                if (claimValue != null)
                    foreach (var item in claimValue)
                    {
                        result.Add(Guid.Parse(item.Value));
                    }
            }
            return result;
        }

        internal static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes != null && attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }


    internal enum ClaimAuthenticatedUser : int
    {
        [Description("Id")]
        Id = 0,
        [Description("IdPerfil")]
        IdPerfil,
        [Description("EnumPerfil")]
        EnumPerfil,
        [Description("Login")]
        Login,
        [Description("Nome")]
        Nome,
        [Description("Email")]
        Email,
        [Description("NovaSenha")]
        NovaSenha,
        [Description("RedefinirSenha")]
        RedefinirSenha

    }
}

