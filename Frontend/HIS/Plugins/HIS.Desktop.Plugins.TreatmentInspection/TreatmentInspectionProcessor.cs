using HIS.Desktop.Plugins.TreatmentInspection.TreatmentInspection;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentInspection
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.TreatmentInspection",
           "Giám định hồ sơ bệnh án",
           "Common",
           4,
           "Giam_dinh_ho_so.png",
           "A",
           Module.MODULE_TYPE_ID__UC,
           true,
           true)
        ]
    public class TreatmentInspectionProcessor : ModuleBase, IDesktopRoot
    {CommonParam param;
        public TreatmentInspectionProcessor()
        {
            param = new CommonParam();
        }
        public TreatmentInspectionProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                ITreatmentInspection behavior = TreatmentInspectionFactory.MakeIControl(param, args);
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
