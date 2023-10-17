using HIS.Desktop.ADO;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using LIS.Desktop.Plugins.TestHistory;
using LIS.EFMODEL.DataModels;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestHistory.TestHistory
{
    class TestHistoryBehavior : Tool<IDesktopToolContext>, ITestHistory
    {
        Inventec.Desktop.Common.Modules.Module Module;
        string patientCode = null;
        internal TestHistoryBehavior()
            : base()
        {

        }

        internal TestHistoryBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, string data)
            : base()
        {
            this.Module = module;
            this.patientCode = data;
        }
        internal TestHistoryBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param)
            : base()
        {
            this.Module = module;
        }

        object ITestHistory.Run()
        {
            object result = null;
            try
            {
                result = new frmTestHistory(Module, this.patientCode);
                if (result == null) throw new NullReferenceException(LogUtil.TraceData("patientCode", patientCode));
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
