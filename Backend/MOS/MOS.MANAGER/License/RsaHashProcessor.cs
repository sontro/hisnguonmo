using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.License
{
    class RsaHashProcessor
    {
        private const string PublicKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCBdRGp9rFsCmx18cCo8N+XSu/4vyFZ9FkobcB9WS7PiHhnBCotuN3rUMoj1iXJuZ1Tqkw7AGjPJ1hBAa6cCMfSQlQX8FVDb+4136eJZp6M7AaNprd/KtnzLB3uIcDAPsjhYKxMcWWY/ov7JqhRSePU7X7NZFCiMA6M+DGsQB3BPQIDAQAB";

        private static string Algorithm = "RSA";//"RSA/ECB/OAEPWithSHA1AndMGF1Padding"
        private const int DefaultRsaBlockSize = 190;
        private int KeySize = 1024;

        public string Decrypt(string encryptedData)
        {
            var decryptionKey = ReadPublicKey(PublicKey);

            var cipher = CipherUtilities.GetCipher(Algorithm);
            cipher.Init(false, decryptionKey);

            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(KeySize);
            System.Security.Cryptography.RSAParameters rp = rsa.ExportParameters(true);
            AsymmetricCipherKeyPair kp = DotNetUtilities.GetRsaKeyPair(rp);
            int blockSize = DefaultRsaBlockSize;// SmallPrimesProduct.BitLength / 8;
            blockSize = cipher.GetBlockSize();

            var dataToDecrypt = Convert.FromBase64String(encryptedData);
            var decryptedData = ApplyCipher(dataToDecrypt, cipher, blockSize);
            string decryptRawString = Convert.ToBase64String(decryptedData).Replace("Af////////////////////////////////////////////////////////////////////////////////////8A", "");
            return Encoding.UTF8.GetString(Convert.FromBase64String(decryptRawString));
        }

        private AsymmetricKeyParameter ReadPublicKey(String key)
        {
            if (!key.StartsWith("-----BEGIN PUBLIC KEY-----"))
            {
                key = "-----BEGIN PUBLIC KEY-----\n" + key;
            }
            if (!key.EndsWith("-----END PUBLIC KEY-----"))
            {
                key = key + "\n-----END PUBLIC KEY-----";
            }
            var byteArray = Encoding.ASCII.GetBytes(key);
            using (var ms = new MemoryStream(byteArray))
            {
                using (var sr = new StreamReader(ms))
                {
                    var pemReader = new Org.BouncyCastle.Utilities.IO.Pem.PemReader(sr);
                    var pem = pemReader.ReadPemObject();
                    var publicKey = PublicKeyFactory.CreateKey(pem.Content);

                    return publicKey;
                }
            }
        }

        private byte[] ApplyCipher(byte[] data, IBufferedCipher cipher, int blockSize)
        {
            var inputStream = new MemoryStream(data);
            var outputBytes = new List<byte>();

            int index;
            var buffer = new byte[(int)blockSize];
            while ((index = inputStream.Read(buffer, 0, (int)blockSize)) > 0)
            {
                var cipherBlock = cipher.DoFinal(buffer, 0, index);
                outputBytes.AddRange(cipherBlock);
            }

            return outputBytes.ToArray();
        }
    }
}
