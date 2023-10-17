using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.VaccinationExamList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.VaccinationExamList",
        "Danh sách yêu cầu khám tiêm chủng",
        "Common",
        62,
        "newitem_32x32.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class VaccinationExamListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public VaccinationExamListProcessor()
        {
            param = new CommonParam();
        }

        public VaccinationExamListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IVaccinationExamList behavior = VaccinationExamListFactory.MakeIVaccinationExamList(param, args);
                result = behavior != null ? (behavior.Run()) : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

    }
}
