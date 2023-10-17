using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Core;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData.Core.CFG
{
    class HisConfigCFG
    {
        internal static bool IsUseZip
        {
            get
            {
                long value = HisConfigs.Get<long>(HisConfigKeys.CONFIG_KEY__HIS_DESKTOP__IS_USE_ZIP);
                return (value == 1);
            }
        }

        internal static bool IsAllowShowingAnapathology
        {
            get
            {
                long value = HisConfigs.Get<long>(HisConfigKeys.CONFIG_KEY__ASSIGN_SERVICE_ALLOW_SHOWING_ANAPATHOGY);
                return (value == 1);
            }
        }

        internal static bool IsUseRedisCacheServer
        {
            get
            {
                long value = HisConfigs.Get<long>(HisConfigKeys.CONFIG_KEY__HIS_IS_USE_REDIS_CACHE_SERVER);
                return (value == 1);
            }
        }
    }
}
