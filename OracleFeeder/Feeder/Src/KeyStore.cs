using log4net;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using TerraSharp.Key;

namespace Feeder
{
    public class KeyStore
    {
        ILog logger = LogManager.GetLogger(typeof(KeyStore));
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

        public Entity[] loadEntities(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string json = reader.ReadToEnd();
                   
                        var entities = JsonConvert.DeserializeObject<Entity[]>(json);
                        return entities is not null ? entities : new Entity[0];
                }
            }
            catch(Exception ex) 
            {
                logger.Error("loadKeys", ex);
                return new Entity[0];
            }
        }

        public async void save(string filePath, string name, string password, string mnemonic) 
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            var keys = loadEntities(filePath);

            if (keys.Select(key => key.Name == name).Any())
            {
                throw new Exception("Key already exists by that name");
            }

            var mnemonicKey = new MnemonicKey(mnemonic, true);

            var cipherText = encrypt(JsonConvert.SerializeObject(mnemonicKey), password);
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