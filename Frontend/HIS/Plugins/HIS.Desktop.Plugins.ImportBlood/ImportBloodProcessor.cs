using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.ImportBlood.ImportBlood;
using Inventec.Common.LocalStorage.SdaConfig;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportBlood
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ImportBlood",
        "Nhập thuốc",
        "Common",
        65,
        "blood.png",
        "A",
        Module.MODULE_TYPE_ID__COMBO,
        true,
        true)]
    public class ImportBloodProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ImportBloodProcessor()
        {
            param = new CommonParam();
        }
        public ImportBloodProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IImportBlood behavior = ImportBloodFactory.MakeIImportBlood(param, args);
                result = (behavior != null) ? behavior.Run() : null;
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
