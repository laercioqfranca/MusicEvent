using System;
using System.Security.Cryptography;
using System.Text;

namespace Log.Domain.Utils
{
    public static class Cryptography
    {
        public static string GetSalt()
        {
            var Number = new byte[32];
            var Generator = RandomNumberGenerator.Create();
            Generator.GetBytes(Number);
            return Convert.ToBase64String(Number);
        }

        public static string GetHash(string Salt, string Password)
        {
            var SHA = SHA256.Create();
            var PasswordBytes = Encoding.UTF8.GetBytes(Salt + Password);
            var Hash = SHA.ComputeHash(PasswordBytes);
            return Convert.ToBase64String(Hash);
        }

        public static string Encrypt(string phrase, string key, string IV)
        {
            Aes cipher = CreateCipher(key);

            cipher.IV = Convert.FromBase64String(IV);

            ICryptoTransform cryptotransform = cipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(phrase);
            byte[] cipherText = cryptotransform.TransformFinalBlock(plainText, 0, plainText.Length);
            return Convert.ToBase64String(cipherText);
        }

        public static string Decrypt(string IV, string encryptedPhrase, string key)
        {
            Aes cipher = CreateCipher(key);

            cipher.IV = Convert.FromBase64String(IV);

            ICryptoTransform cryptoTransform = cipher.CreateDecryptor();
            byte[] encryptedBytes = Convert.FromBase64String(encryptedPhrase);
            byte[] plainBytes = cryptoTransform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }

        private static Aes CreateCipher(string key64)
        {
            Aes cipher = Aes.Create();
            cipher.Mode = CipherMode.CBC;
            cipher.Padding = PaddingMode.ISO10126;
            cipher.Key = Convert.FromBase64String(key64);

            return cipher;
        }

        public static string InitiateIV()
        {
            Aes cipher = Aes.Create();
            var IVBase64 = Convert.ToBase64String(cipher.IV);
            return IVBase64;
        }

    }
}
