using System.Security.Cryptography;
using System.Text;
using ETicaretAPI.Application.Abstractions.Encrypt;

namespace ETicaretAPI.Infrastructure.Services.Encrypt
{
    public class EncryptionService : IEncryptionService
    {
        // AES (Simetrik) Şifreleme
        public string EncryptAES(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key); // 16, 24 veya 32 byte anahtar uzunluğu
                aesAlg.IV = new byte[16]; // IV sıfırla

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        // AES (Simetrik) Çözme
        public string DecryptAES(string cipherText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[16];

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        // RSA (Asimetrik) Şifreleme
        public string EncryptRSA(string plainText, string publicKey)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.FromXmlString(publicKey); // Public key'yi RSA nesnesine yükleyin

                byte[] encryptedBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText), RSAEncryptionPadding.OaepSHA256);
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        // RSA (Asimetrik) Çözme
        public string DecryptRSA(string cipherText, string privateKey)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.FromXmlString(privateKey); // Private key'i RSA nesnesine yükleyin

                byte[] decryptedBytes = rsa.Decrypt(Convert.FromBase64String(cipherText), RSAEncryptionPadding.OaepSHA256);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

    }
}
