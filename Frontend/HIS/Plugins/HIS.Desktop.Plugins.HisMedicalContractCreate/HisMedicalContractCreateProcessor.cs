using HIS.Desktop.Plugins.HisMedicalContractCreate.HisMedicalContractCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMedicalContractCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.HisMedicalContractCreate",
        "Hợp đồng thầu",
        "Bussiness",
        4,
        "showproduct_32x32.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)
     ]
    public class HisMedicalContractCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisMedicalContractCreateProcessor()
        {
            param = new CommonParam();
        }
        public HisMedicalContractCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IHisMedicalContractCreate behavior = HisMedicalContractCreateFactory.MakeICrateType(param, args);
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
