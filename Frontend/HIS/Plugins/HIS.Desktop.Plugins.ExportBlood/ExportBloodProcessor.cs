using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.ExportBlood.ExportBlood;
using Inventec.Common.LocalStorage.SdaConfig;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportBlood
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ExportBlood",
        "Xuất máu",
        "Common",
        62,
        "exportBlood.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class ExportBloodProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExportBloodProcessor()
        {
            param = new CommonParam();
        }
        public ExportBloodProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IExportBlood behavior = ExportBloodFactory.MakeIExportBlood(param, args);
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
            return false;
        }
    }
}
