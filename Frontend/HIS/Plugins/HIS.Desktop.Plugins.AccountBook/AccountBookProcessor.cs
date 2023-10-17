using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Common;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.HisAccountBookList.AccountBook;
using Inventec.Common.LocalStorage.SdaConfig;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.HisAccountBookList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.HisAccountBookList",
       "Sổ thu chi",
       "Common",
       16,
       "so-hoa-don.png",
       "A",
       Module.MODULE_TYPE_ID__UC,
       true,
       true)
    ]

    public class AccountBookProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public AccountBookProcessor()
        {
            param = new CommonParam();
        }
        public AccountBookProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IAccountBook behavior = AccountBookFactory.MakeIAccountBook(param, args);
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
