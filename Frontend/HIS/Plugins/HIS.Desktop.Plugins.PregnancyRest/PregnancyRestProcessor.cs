using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PregnancyRest
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
    "HIS.Desktop.Plugins.PregnancyRest",
    "Nhập thông tin chứng nhận nghỉ dưỡng thai",
    "Common",
    62,
    "MedicineList.png",
    "A",
    Module.MODULE_TYPE_ID__FORM,
    true,
    true)]
    public class PregnancyRestProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public PregnancyRestProcessor()
        {
            param = new CommonParam();
        }
        public PregnancyRestProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                PregnancyRest.IPregnancyRest behavior = PregnancyRest.PregnancyRestFactory.MakeIPregnancyRest(param, args);
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
