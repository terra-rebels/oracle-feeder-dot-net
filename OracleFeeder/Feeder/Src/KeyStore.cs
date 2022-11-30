using System.Security.Cryptography;
using System.Text;

namespace Feeder
{
    public class KeyStore
    {
        public interface Entity
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string CipherText { get; set; }
        }

        public interface PlainEntity
        {
            public string PrivateKey { get; set; }
            public string PublicKey { get; set; }
            public string TerraAddress { get; set; }
            public string TerraValAdress { get; set; }
        }

        public string encrypt(string plainText, string pass)
        {
            var key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(pass));
            var encryption = ComputeEncryption(key, plainText);
            return Convert.ToHexString(encryption);
        }
        private byte[] ComputeEncryption(byte[] key, string plainText)
        {
            Aes aes = Aes.Create();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = key;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            byte[] data = Encoding.UTF8.GetBytes(plainText);

            using (ICryptoTransform encrypt = aes.CreateEncryptor())
            {
                return encrypt.TransformFinalBlock(data, 0, data.Length);
            }
        }
    }
}