using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.MedicineList.MedicineList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.MedicineList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.MedicineList",
           "Danh sách thuốc",
           "Common",
           16,
           "thuoc.png",
           "E",
           Module.MODULE_TYPE_ID__UC,
           true,
           true)
    ]
    public class MedicineListProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MedicineListProcessor()
        {
            param = new CommonParam();
        }
        public MedicineListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IMedicineList behavior = MedicineListFactory.MakeIMedicineList(param, args);
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
