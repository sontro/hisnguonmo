using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using HIS.Desktop.Plugins.HisSourceMedicine;
using HIS.Desktop.Plugins.HisSourceMedicine.HisSourceMedicine;

namespace HIS.Desktop.Plugins.HisSourceMedicine
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
    "HIS.Desktop.Plugins.HisSourceMedicine",
    "Danh mục",
    "Bussiness",
    6,
    "",
    "A",
    Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__FORM,
    true,
    true)
]
    class HisSourceMedicineProcessors : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisSourceMedicineProcessors()
        {
            param = new CommonParam();
        }

        public HisSourceMedicineProcessors(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IHisSourceMedicine behavior = HisSourceMedicineFactory.MakeIControl(param, args);
                result = behavior != null ? (object)(behavior.Run()) : null;
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
