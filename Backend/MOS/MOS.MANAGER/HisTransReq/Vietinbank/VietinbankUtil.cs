using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransReq.Vietinbank
{
    class VietinbankUtil
    {
        internal const string PaymentCode__Success = "00";
        internal const string PaymentCode__HasPay = "01";
        internal const string PaymentCode__Invalid = "02";
        internal const string PaymentCode__NotFound = "03";
        internal const string PaymentCode__InvalidAmount = "04";
        internal const string PaymentCode__Expired = "05";
        internal const string PaymentCode__TimeOut = "08";
        internal const string PaymentCode__Exception = "99";

        internal static string VietinbankFileCer = ConfigurationManager.AppSettings["MOS.MANAGER.Vietinbank.CertificatePath"];
        internal static string VietinbankHashAlg = ConfigurationManager.AppSettings["MOS.MANAGER.Vietinbank.HashAlg"];

        internal static string InventecFileCer = ConfigurationManager.AppSettings["MOS.MANAGER.Inventec.CertificatePath"];
        internal static string InventecPass = ConfigurationManager.AppSettings["MOS.MANAGER.Inventec.CertificatePass"];

        internal static string CheckSignature = ConfigurationManager.AppSettings["MOS.MANAGER.Vietinbank.NotCheckSignature"];
    }
}
