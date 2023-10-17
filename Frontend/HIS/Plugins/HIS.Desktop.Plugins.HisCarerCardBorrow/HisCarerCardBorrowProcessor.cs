using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.HisCarerCardBorrow
{
    class HisCarerCardBorrowProcessor
    {
        [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.HisCarerCardBorrow",
           "Quản lý mượn thẻ",
           "",
           0,
           "",
           "",
           Module.MODULE_TYPE_ID__UC,
           true,
           true)
        ]
        public class HisCarerCardBorrowQProcessor : ModuleBase, IDesktopRoot
        {
            CommonParam param;
            public HisCarerCardBorrowQProcessor()
            {
                param = new CommonParam();
            }
            public HisCarerCardBorrowQProcessor(CommonParam paramBusiness)
            {
                param = (paramBusiness != null ? paramBusiness : new CommonParam());
            }

            public object Run(object[] args)
            {
                object result = null;
                try
                {
                    HisCarerCardBorrow.IHisCarerCardBorrow behavior = HisCarerCardBorrow.HisCarerCardBorrowFactory.MakeIHisCarerCardBorrow(param, args);
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
}
