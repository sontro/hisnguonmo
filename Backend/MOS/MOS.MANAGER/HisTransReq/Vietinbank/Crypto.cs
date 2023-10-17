using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransReq.Vietinbank
{
    class Crypto
    {
        /// <summary>
        /// This function is used to sign string data.
        /// </summary>
        /// <param name="data">String for signing</param>
        /// <param name="privatekey">Full filename to Private key</param>
        /// <param name="passpharse">Passpharse to access private key</param>
        /// <param name="hashAlg">SHA1-SHA256-SHA512</param>
        /// <returns>String base64</returns>
        public static string Sign(string data, string privatekey, string passpharse, string hashAlg)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            RSACryptoServiceProvider rsaCSP = new RSACryptoServiceProvider();
            X509Certificate2 cert = new X509Certificate2(privatekey, passpharse, X509KeyStorageFlags.Exportable);
            rsaCSP.FromXmlString(cert.PrivateKey.ToXmlString(true));

            return Convert.ToBase64String(rsaCSP.SignData(encoding.GetBytes(data), CryptoConfig.MapNameToOID(hashAlg)));
        }

        /// <summary>
        /// This function is used to verify string data.
        /// </summary>
        /// <param name="signedData">Signature value (string base64)</param>
        /// <param name="data">String for signing</param>
        /// <param name="publickey">Full filename to Public key</param>
        /// <param name="hashAlg">SHA1 or SHA256</param>
        /// <returns>true: verify successful; false: verify fail</returns>
        public static bool Verify(string signedData, string data, string publickey, string hashAlg)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            RSACryptoServiceProvider rsaCSP = new RSACryptoServiceProvider();

            byte[] bufData = encoding.GetBytes(data);

            byte[] bufSigned = Convert.FromBase64String(signedData);

            X509Certificate2 cert = new X509Certificate2(publickey);
            rsaCSP = (RSACryptoServiceProvider)cert.PublicKey.Key;

            return rsaCSP.VerifyData(bufData, CryptoConfig.MapNameToOID(hashAlg), bufSigned);
        }
    }
}
