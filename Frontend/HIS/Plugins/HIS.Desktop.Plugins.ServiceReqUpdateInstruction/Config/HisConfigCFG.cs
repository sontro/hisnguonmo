using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqUpdateInstruction.Config
{
    public class HisConfigCFG
    {
        internal const string CONFIG_KEY__StartTimeMustBeGreaterThanInstructionTime = "HIS.Desktop.Plugins.StartTimeMustBeGreaterThanInstructionTime";

        internal static string StartTimeMustBeGreaterThanInstructionTime
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(CONFIG_KEY__StartTimeMustBeGreaterThanInstructionTime);
            }
        }
    }
}
