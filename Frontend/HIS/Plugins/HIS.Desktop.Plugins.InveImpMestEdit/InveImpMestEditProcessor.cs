using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InveImpMestEdit
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
          "HIS.Desktop.Plugins.InveImpMestEdit",
          "Sửa phiếu nhập kiểm kê",
          "Common",
          16,
          "impMestCreate.png",
          "E",
          Module.MODULE_TYPE_ID__FORM,
          true,
          true)
       ]
    public class InveImpMestEditProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;

        public InveImpMestEditProcessor()
        {
            param = new CommonParam();
        }

        public InveImpMestEditProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                InveImpMestEdit.IInveImpMestEdit behavior = InveImpMestEdit.InveImpMestEditFactory.MakeIInveImpMestEdit(param, args);
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
