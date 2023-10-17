using HIS.Desktop.Plugins.EpidemiologyInfo.EpidemiologyInfo;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EpidemiologyInfo
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.EpidemiologyInfo",
        "Thông tin dịch tễ",
        "Bussiness",
        4,
        "showproduct_32x32.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)
    ]
    public class EpidemiologyInfoProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public EpidemiologyInfoProcessor()
        {
            param = new CommonParam();
        }
        public EpidemiologyInfoProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IEpidemiologyInfo behavior = EpidemiologyInfoFactory.MakeIEpidemiologyInfo(param, args);
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
