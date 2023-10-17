using HIS.Desktop.Plugins.NotTakenApprove.NotTakenApprove;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.NotTakenApprove
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
          "HIS.Desktop.Plugins.NotTakenApprove",
          "Danh sách xuất",
          "Common",
          16,
          "xuat-kho.png",
          "E",
          Module.MODULE_TYPE_ID__FORM,
          true,
          true)
       ]

    public class NotTakenApproveProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public NotTakenApproveProcessor()
        {
            param = new CommonParam();
        }
        public NotTakenApproveProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                INotTakenApprove behavior = NotTakenApproveFactory.MakeINotTakenApprove(param, args);
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
