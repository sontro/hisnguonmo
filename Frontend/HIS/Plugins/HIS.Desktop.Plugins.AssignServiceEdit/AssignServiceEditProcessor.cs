using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignServiceEdit
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.AssignServiceEdit",
        "Sửa chỉ định dịch vụ",
        "Common",
        62,
        "highlightfield_16x16.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class AssignServiceEditProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public AssignServiceEditProcessor()
        {
            param = new CommonParam();
        }

        public AssignServiceEditProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                AssignServiceEdit.IAssignServiceEdit behavior = AssignServiceEdit.AssignServiceEditFactory.MakeIAssignServiceEdit(param, args);
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
