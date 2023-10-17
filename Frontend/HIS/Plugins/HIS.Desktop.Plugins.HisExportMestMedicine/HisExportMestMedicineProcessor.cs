using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.HisExportMestMedicine.HisExportMestMedicine;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.HisExportMestMedicine
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
          "HIS.Desktop.Plugins.HisExportMestMedicine",
          "Danh sách xuất",
          "Common",
          16,
          "xuat-kho.png",
          "E",
          Module.MODULE_TYPE_ID__UC,
          true,
          true)
       ]

    public class HisExportMestMedicineProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisExportMestMedicineProcessor()
        {
            param = new CommonParam();
        }
        public HisExportMestMedicineProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IHisExportMestMedicine behavior = HisExportMestMedicineFactory.MakeIHisExportMestMedicine(param, args);
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
