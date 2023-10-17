using HIS.Desktop.Plugins.PeriodList.PeriodList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PeriodList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.PeriodList",
        "Danh Sách Kỳ",
        "Common",
        51,
        "calendar_32x32.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class PeriodListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public PeriodListProcessor()
        {
            param = new CommonParam();
        }
        public PeriodListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IPeriodList behavior = PeriodListFactory.MakeIPeriodList(param, null);
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
