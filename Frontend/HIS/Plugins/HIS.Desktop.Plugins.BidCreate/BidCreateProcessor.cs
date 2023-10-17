using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.BidCreate.BidCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.BidCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.BidCreate",
       "Tạo gói thầu",
       "Common",
       16,
       "thau.png",
       "C",
       Module.MODULE_TYPE_ID__UC,
       true,
       true)
    ]

    public class BidCreateProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public BidCreateProcessor()
        {
            param = new CommonParam();
        }
        public BidCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IBidCreate behavior = BidCreateFactory.MakeIBidCreate(param, args);
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
