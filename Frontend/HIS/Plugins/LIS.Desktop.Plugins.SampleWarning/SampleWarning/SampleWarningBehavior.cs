using HIS.Desktop.ADO;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using LIS.Desktop.Plugins.SampleWarning;
using LIS.EFMODEL.DataModels;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SampleWarning.SampleWarning
{
    class SampleWarningBehavior : Tool<IDesktopToolContext>, ISampleWarning
    {
        Inventec.Desktop.Common.Modules.Module Module;
        internal SampleWarningBehavior()
            : base()
        {

        }

        internal SampleWarningBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param)
            : base()
        {
            this.Module = module;
        }

        object ISampleWarning.Run()
        {
            object result = null;
            try
            {
                result = new frmSampleWarning(Module);
                if (result == null) throw new NullReferenceException(LogUtil.TraceData("Module", Module));
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
