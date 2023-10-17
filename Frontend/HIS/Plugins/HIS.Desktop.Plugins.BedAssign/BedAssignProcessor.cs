using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BedAssign
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
          "HIS.Desktop.Plugins.BedAssign",
          "Gán giường",
          "Common",
          62,
          "",
          "A",
          Module.MODULE_TYPE_ID__FORM,
          true,
          true)]
    public class BedAssignProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public BedAssignProcessor()
        {
            param = new CommonParam();
        }

        public BedAssignProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                BedAssign.IBedAssign behavior = BedAssign.BedAssignFactory.MakeIBedAssign(param, args);
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
