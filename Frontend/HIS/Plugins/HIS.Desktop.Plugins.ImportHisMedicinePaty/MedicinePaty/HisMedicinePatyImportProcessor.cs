using System;
using System.Collections.Generic;
using Inventec.Desktop.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;
using HIS.Desktop.Plugins.ImportHisMedicinePaty.MedicinePaty.Run;

namespace HIS.Desktop.Plugins.ImportHisMedicinePaty.MedicinePaty
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.ImportHisMedicinePaty",
       "Import",
       "Common",
       14,
       "pivot_32x32.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true
       )
    ]
    public class HisImportMedicinePatyProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisImportMedicinePatyProcessor()
        {
            param = new CommonParam();
        }
        public HisImportMedicinePatyProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IHisImportMedicinePaty behavior = HisMedicineImportFactory.MakeIHisImportMedicinePaty(param, args);
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
