using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.ServiceHeinBHYT.ServiceHeinBHYT;
using Inventec.Common.LocalStorage.SdaConfig;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceHeinBHYT
{
     [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ServiceHeinBHYT",
        "Danh mục dịch vụ BHYT",
        "Common",
        57,
        "showproduct_32x32.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class ServiceHeinBHYTProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ServiceHeinBHYTProcessor()
        {
            param = new CommonParam();
        }
        public ServiceHeinBHYTProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }
        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IServiceHeinBHYT behavior = ServiceHeinBHYTFactory.MakeIServiceHeinBHYT(param, args);
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
            bool result = false;
            try
            {
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }

            return result;
        }
    }
}
