using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ServiceExecute.ServiceExecute;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.ServiceExecute
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ServiceExecute",
        "Xử lý dịch vụ",
        "Common",
        16,
        "weightedpies_32x32.png",
        "E",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)
    ]
    public class ServiceExecuteProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ServiceExecuteProcessor()
        {
            param = new CommonParam();
        }
        public ServiceExecuteProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IServiceExecute behavior = ServiceExecuteFactory.MakeIServiceExecute(param, args);
                result = behavior != null ? (behavior.Run()) : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
        public override bool IsEnable()
        {
            return true;
        }
    }
}
