using HIS.Desktop.Plugins.CheckInfoBHYT.CheckInfoBHYT;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CheckInfoBHYT
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.CheckInfoBHYT",
        "Kiểm tra thông tin thẻ BHYT",
        "Common",
        59,
        "CheckInfoBHYT.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class CheckInfoBHYTProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public CheckInfoBHYTProcessor()
        {
            param = new CommonParam();
        }
        public CheckInfoBHYTProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ICheckInfoBHYT behavior = CheckInfoBHYTFactory.MakeICheckInfoBHYT(param, args);
                result = (behavior != null) ? behavior.Run() : null;
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
            return false;
        }
    }
}
