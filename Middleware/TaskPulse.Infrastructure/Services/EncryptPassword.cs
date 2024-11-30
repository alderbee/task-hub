using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TaskPulse.Domain.Options;

namespace TaskPulse.Infrastructure.Services;

public class EncryptPassword : IEncryptPassword
{
    private EncryptPasswordOptions encryptPasswordOptions;

    private string hashKey => encryptPasswordOptions.PasswordHashKey;

    public EncryptPassword(IOptions<EncryptPasswordOptions> encryptPasswordOptions)
    {
        this.encryptPasswordOptions = encryptPasswordOptions.Value;
    }
    
    public string Encrypt(string password)
    {
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = Encoding.UTF8.GetBytes(hashKey);
        
        aesAlg.IV = new byte[16]; // Used a random IV (Initialization Vector) for added security
        MemoryStream msEncrypt = new MemoryStream();

        using (msEncrypt)
        {
            using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(password);
                    csEncrypt.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
                    csEncrypt.FlushFinalBlock();
                }
            }
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }

}