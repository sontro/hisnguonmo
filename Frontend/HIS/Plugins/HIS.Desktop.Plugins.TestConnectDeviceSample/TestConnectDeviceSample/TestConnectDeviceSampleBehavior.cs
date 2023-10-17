using HIS.Desktop.Plugins.TestConnectDeviceSample.Base;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestConnectDeviceSample.TestConnectDeviceSample
{
    class TestConnectDeviceSampleBehavior : Tool<IDesktopToolContext>, ITestConnectDeviceSample
    {
        Inventec.Desktop.Common.Modules.Module Module;

        internal TestConnectDeviceSampleBehavior()
            : base()
        {

        }

        internal TestConnectDeviceSampleBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param)
            : base()
        {
            this.Module = module;
        }

        object ITestConnectDeviceSample.Run()
        {
            object result = null;
            try
            {
                result = new UCTestConnectDeviceSample(Module);
                if (result == null) throw new NullReferenceException(Inventec.Common.Logging.LogUtil.TraceData("Module", Module));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
