using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDataStore;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    public class HisKskContractCFG
    {
        private const string RESTRICTED_ACCESSING_CFG = "MOS.HIS_KSK_CONSTRACT.RESTRICTED_ACCESSING";

        private static bool? restrictedAccessing;
        public static bool RESTRICTED_ACCESSING
        {
            get
            {
                if (!restrictedAccessing.HasValue)
                {
                    restrictedAccessing = ConfigUtil.GetIntConfig(RESTRICTED_ACCESSING_CFG) == 1;
                }
                return restrictedAccessing.Value;
            }
        }

        public static void Reload()
        {
            restrictedAccessing = ConfigUtil.GetIntConfig(RESTRICTED_ACCESSING_CFG) == 1;
        }
    }
}