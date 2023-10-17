using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.MobaImpMestList.MobaImpMestList;
using Inventec.Common.LocalStorage.SdaConfig;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.MobaImpMestList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.MobaImpMestList",
       "Danh sách nhập",
       "Common",
       16,
       "nhap-kho.png",     
       "A",
       Module.MODULE_TYPE_ID__COMBO,
       true,
       true)
    ]

    public class HisImportMestMedicineProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisImportMestMedicineProcessor()
        {
            param = new CommonParam();
        }
        public HisImportMestMedicineProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IMobaImpMestList behavior = MobaImpMestListFactory.MakeIHisImportMestMedicine(param, args);
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
