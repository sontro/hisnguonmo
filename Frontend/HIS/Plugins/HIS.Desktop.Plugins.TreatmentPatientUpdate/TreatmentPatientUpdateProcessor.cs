using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.TreatmentPatientUpdate.TreatmentPatientUpdate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.TreatmentPatientUpdate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
    "HIS.Desktop.Plugins.TreatmentPatientUpdate",
    "Ghép mã bệnh nhân",
    "Common",
    70,
    "MedicineList.png",
    "A",
    Module.MODULE_TYPE_ID__FORM,
    true,
    true)]

    public class TreatmentPatientUpdateProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TreatmentPatientUpdateProcessor()
        {
            param = new CommonParam();
        }
        public TreatmentPatientUpdateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITreatmentPatientUpdate behavior = TreatmentPatientUpdateFactory.MakeITreatmentPatientUpdate(param, args);
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