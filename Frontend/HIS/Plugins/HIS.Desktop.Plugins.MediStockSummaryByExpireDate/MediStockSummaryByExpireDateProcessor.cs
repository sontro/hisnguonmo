using Inventec.Core;
using HIS.Desktop.Common;
using Inventec.Desktop.Core;
using Inventec.Desktop.Plugins.MediStockSummaryByExpireDate.MediStockSummaryByExpireDate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Common.Modules;
using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Common.LocalStorage.SdaConfig;

namespace Inventec.Desktop.Plugins.MediStockSummaryByExpireDate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.MediStockSummaryByExpireDate",
       "Tồn kho hạn sử dụng",
       "Common",
       15,
       "kho.png",
       "A",
       Module.MODULE_TYPE_ID__UC,
       true,
       true
       )
    ]
    public class MediStockSummaryByExpireDateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MediStockSummaryByExpireDateProcessor()
        {
            param = new CommonParam();
        }
        public MediStockSummaryByExpireDateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IMediStockSummaryByExpireDate behavior = MediStockSummaryByExpireDateFactory.MakeIMediStockSummaryByExpireDate(param, args);
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
