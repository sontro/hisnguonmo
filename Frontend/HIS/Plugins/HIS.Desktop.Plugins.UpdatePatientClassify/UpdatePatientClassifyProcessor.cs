using HIS.Desktop.Plugins.UpdatePatientClassify.UpdatePatientClassify;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.UpdatePatientClassify
{
        [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.UpdatePatientClassify",
        "Cập nhật thông tin phân loại bênh nhận",
        "Common",
        62,
        "newitem_32x32.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class UpdatePatientClassifyProcessor: ModuleBase, IDesktopRoot
    {
            CommonParam param;
        public UpdatePatientClassifyProcessor()
        {
            param = new CommonParam();
        }

        public UpdatePatientClassifyProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IUpdatePatientClassify behavior = UpdatePatientClassifyFactory.MakeIUpdatePatientClassify(param, args);
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
