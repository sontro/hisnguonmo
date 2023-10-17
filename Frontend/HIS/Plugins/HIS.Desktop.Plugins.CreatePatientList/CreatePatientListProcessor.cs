using HIS.Desktop.Plugins.CreatePatientList.CreatePatientList;
using Inventec.Core;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CreatePatientList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.CreatePatientList",
        "Sửa thông tin bệnh nhân",
        "Common",
        62,
        "pivot_32x32.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class CreatePatientListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public CreatePatientListProcessor()
        {
            param = new CommonParam();
        }
        public CreatePatientListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ICreatePatientList behavior = CreatePatientListFactory.MakeICreatePatientList(param, args);
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
