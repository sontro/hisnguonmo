using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PublicMedicineGeneral
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
         "HIS.Desktop.Plugins.PublicMedicineGeneral",
         "Tạo công khai thuốc",
         "Common",
         23,
         "thuoc.png",
         "A",
         Module.MODULE_TYPE_ID__FORM,
         true,
         true
         )
      ]
    public class PublicMedicineGeneralProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public PublicMedicineGeneralProcessor()
        {
            param = new CommonParam();
        }
        public PublicMedicineGeneralProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {

                PublicMedicineGeneral.IPublicMedicineGeneral behavior = PublicMedicineGeneral.PublicMedicineGeneralFactory.MakeIPublicMedicineGeneral(param, args);
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
