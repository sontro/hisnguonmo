using MOS.EFMODEL.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.Token;
using Inventec.Common.Logging;
using System.Configuration;

namespace MOS.MANAGER.Config
{
	public class SystemCFG
	{
		private const string IS_USING_SERVER_TIME_CFG = "MOS.IS_USING_SERVER_TIME";
        private const string MASTER_ADDRESS_CFG = "MOS.MASTER_ADDRESS";
        private const string SLAVE_ADDRESSES_CFG = "MOS.SLAVE_ADDRESSES";

		private static bool? isUsingServerTime;
		public static bool IS_USING_SERVER_TIME
		{
			get
			{
				if (!isUsingServerTime.HasValue)
				{
					isUsingServerTime = ConfigUtil.GetIntConfig(IS_USING_SERVER_TIME_CFG) == 1;
				}
				return isUsingServerTime.Value;
			}
		}

        private static string masterAddress;
        public static string MASTER_ADDRESS
        {
            get
            {
                if (String.IsNullOrWhiteSpace(masterAddress))
                {
                    masterAddress = ConfigUtil.GetStrConfig(MASTER_ADDRESS_CFG);
                }
                return masterAddress;
            }
        }

        private static List<string> slaveAddresses;
        public static List<string> SLAVE_ADDRESSES
        {
            get
            {
                if (slaveAddresses == null)
                {
                    slaveAddresses = ConfigUtil.GetStrConfigs(SLAVE_ADDRESSES_CFG);
                }
                return slaveAddresses;
            }
        }

        private static bool? isSlave;
        public static bool IS_SLAVE
        {
            get
			{
                if (!isSlave.HasValue)
				{
                    try
                    {
                        return ConfigurationManager.AppSettings["MOS.LoadBalancing.IsSlave"] == "1";
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(ex);
                        return false;
                    }
                }
                return isSlave.Value;
            }
        }

		public static void Reload()
		{
			isUsingServerTime = ConfigUtil.GetIntConfig(IS_USING_SERVER_TIME_CFG) == 1;
            slaveAddresses = ConfigUtil.GetStrConfigs(SLAVE_ADDRESSES_CFG);
            masterAddress = ConfigUtil.GetStrConfig(MASTER_ADDRESS_CFG);
		}
	}
}
