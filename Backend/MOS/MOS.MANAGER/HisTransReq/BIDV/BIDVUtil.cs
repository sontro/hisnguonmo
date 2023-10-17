using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace MOS.MANAGER.HisTransReq.BIDV
{
    class BIDVUtil
    {
        internal const decimal PRICE_DIFFERENCE = 1m;

        internal const string PaymentCode__Success = "00";
        internal const string PaymentCode__ProductsNotEnough = "01";
        internal const string PaymentCode__ProductsOutOfStock = "02";
        internal const string PaymentCode__AlreadyPaid = "03";
        internal const string PaymentCode__PaymentExeption = "04";
        internal const string PaymentCode__PaymentProcessing = "05";
        internal const string PaymentCode__AuthenticationFailed = "06";
        internal const string PaymentCode__IncorrectAmount = "07";
        internal const string PaymentCode__TimeOut = "08";
        internal const string PaymentCode__QRTimeOut = "09";
        internal const string PaymentCode__InvalidInput = "11";
        internal const string PaymentCode__IPisLocked = "14";
        internal const string PaymentCode__DontPostToMerchant = "88";
        internal const string PaymentCode__SystemIsMaintaining = "96";

        internal static string BIDVSecretKey = ConfigurationManager.AppSettings["MOS.MANAGER.BIDV.SECRET_KEY"];

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
