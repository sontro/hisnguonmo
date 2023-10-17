using HIS.Desktop.Plugins.HisServiceReReInDiffDay.HisServiceReReInDiffDay;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServiceReReInDiffDay
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.HisServiceReReInDiffDay",
           "Thiết lập trả kết quả khác ngày",
           "Danh mục",
           4,
           "thiet-lap.png",
           "A",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
        ]

    public class HisServiceReReInDiffDayProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisServiceReReInDiffDayProcessor()
        {
            param = new CommonParam();
        }
        public HisServiceReReInDiffDayProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IHisServiceReReInDiffDay behavior = HisServiceReReInDiffDayFactory.MakeIControl(param, args);
                result = behavior != null ? (object)(behavior.Run()) : null;
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
            bool result = false;
            try
            {
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
