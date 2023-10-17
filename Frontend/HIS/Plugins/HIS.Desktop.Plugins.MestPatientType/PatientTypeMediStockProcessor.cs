using HIS.Desktop.Plugins.MestPatientType.PatientTypeMediStock;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;

namespace HIS.Desktop.Plugins.MestPatientType
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.MestPatientType",
        "Kho - đối tượng bệnh nhân",
        "Common",
        62,
        "technology_32x32.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class PatientTypeMediStockProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public PatientTypeMediStockProcessor()
        {
            param = new CommonParam();
        }
        public PatientTypeMediStockProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IPatientTypeMediStock behavior = PatientTypeMediStockFactory.MakeIPatientTypeMediStock(param, args);
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