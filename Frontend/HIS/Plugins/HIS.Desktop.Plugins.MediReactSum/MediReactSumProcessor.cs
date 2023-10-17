using HIS.Desktop.Plugins.MediReactSum.MediReactSum;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediReactSum
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.MediReactSum",
        "Phản ứng thuốc",
        "Common",
        59,
        "MediReactSum.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class MediReactSumProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MediReactSumProcessor()
        {
            param = new CommonParam();
        }
        public MediReactSumProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IMediReactSum behavior = MediReactSumFactory.MakeIMediReactSum(param, args);
                result = (behavior != null) ? behavior.Run() : null;
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
