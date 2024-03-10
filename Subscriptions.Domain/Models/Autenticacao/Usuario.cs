using Subscriptions.Core.Models;
using Subscriptions.Domain.Utils;
using System;
using System.Collections.Generic;

namespace Subscriptions.Domain.Models.Autenticacao
{
    public class Usuario : Entity
    {
        public Usuario()
        {
        }

        public string Nome { get; private set; }
        public int Idade { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Salt { get; private set; }
        public string Email { get; private set; }
        public Guid? IdPerfil { get; private set; }      
        public DateTime DataInclusao { get; private set; }
        public bool Excluido { get; private set; }

        public virtual PerfilUsuario Perfil { get; private set; }
        public virtual IEnumerable<Inscricao> EventoUsuarios { get; set; }

        public void setUsuario(Guid id, string nome, int idade, string senha, string email, Guid? idPerfil)
        {
            Id = id;
            Nome = nome;
            Idade = idade;
            Email = email;
            IdPerfil = Guid.Parse("E3430F1F-D2D4-4A88-9E82-6C9F475F3A80"); //Perfil cliente.          
            Login = email;
            DataInclusao = DateTime.Now;
            setCriptografia(senha, null);
        }

        public void setExcluido(bool excluido)
        {
            Excluido = excluido;
        }

        public string setGerarSenhaAleatoria()
        {
            string senhagerada = CreateRandomPassword();
            setCriptografia(senhagerada, "");
            return senhagerada;
        }

        public void setCriptografia(string senha, string salt)
        {
            Salt = string.IsNullOrEmpty(salt) ? Cryptography.GetSalt() : salt;
            Senha = Cryptography.GetHash(Salt, string.IsNullOrEmpty(senha) ? "Subscriptions.@2023" : senha);
        }

        public void setUpdateUsuario(string nome, int idade, string email, Guid? idPerfil)
        {
            Nome = nome;
            Idade = idade;
            Email = email;
            IdPerfil = idPerfil;
            Perfil = null;
        }

        private static string CreateRandomPassword(int length = 8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
    }
}