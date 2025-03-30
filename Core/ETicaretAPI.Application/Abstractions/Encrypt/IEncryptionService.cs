namespace ETicaretAPI.Application.Abstractions.Encrypt
{
    public interface IEncryptionService
    {
        // AES (Simetrik) Şifreleme
        string EncryptAES(string plainText, string key);

        // AES (Simetrik) Çözme
        string DecryptAES(string cipherText, string key);

        // RSA (Asimetrik) Şifreleme
        string EncryptRSA(string plainText, string publicKey);

        // RSA (Asimetrik) Çözme
        string DecryptRSA(string cipherText, string privateKey);
    }
}
