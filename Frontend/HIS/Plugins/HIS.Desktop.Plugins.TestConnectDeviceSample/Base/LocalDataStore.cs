using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestConnectDeviceSample.Base
{
    internal class LocalDataStore
    {
        private static string ConfigTimeout = System.Configuration.ConfigurationSettings.AppSettings["HIS.Desktop.Transaction.SelectAccount.Timeout"];
        internal static string MessageFromDevice { get; set; }

        internal const int TIME_OUT_AWAIT_MESSAGE_DEVICE = 10000;
        internal const short IS_TRUE = (short)1;

        private static int? selectAccountTimeout;
        internal static int SelectAccountTimeout
        {
            get
            {
                if (!selectAccountTimeout.HasValue)
                {
                    try
                    {
                        string second = String.IsNullOrWhiteSpace(ConfigTimeout) ? "10000" : ConfigTimeout;
                        selectAccountTimeout = Convert.ToInt32(second);
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(ex);
                        selectAccountTimeout = 10000;
                    }
                }
                return selectAccountTimeout.Value;
            }
        }
    }
}
