using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisKskDriver.Sync
{
    class SyncConfig
    {
        internal static string GATEWAY_KSK_USERNAME = ConfigurationManager.AppSettings["Inventec.DuLieuYTe.Username"];
        internal static string GATEWAY_KSK_PASSWORD = ConfigurationManager.AppSettings["Inventec.DuLieuYTe.Password"];
        internal static string GATEWAY_KSK_API_URI = ConfigurationManager.AppSettings["Inventec.DuLieuYTe.ApiUri"];
        internal static string GATEWAY_KSK_BASE_ADDRESS = ConfigurationManager.AppSettings["Inventec.DuLieuYTe.BaseAddress"];
    }
}
