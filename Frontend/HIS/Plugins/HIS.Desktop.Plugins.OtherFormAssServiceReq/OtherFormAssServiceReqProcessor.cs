using HIS.Desktop.Plugins.OtherFormAssServiceReq.OtherFormAssServiceReq;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.OtherFormAssServiceReq
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.OtherFormAssServiceReq",
       "Biểu mẫu khác",
       "Common",
       23,
       "pivot_32x32.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true
       )
    ]
    public class OtherFormAssServiceReqProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public OtherFormAssServiceReqProcessor()
        {
            param = new CommonParam();
        }
        public OtherFormAssServiceReqProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IOtherFormAssServiceReq behavior = OtherFormAssServiceReqFactory.MakeIOtherFormAssServiceReq(param, args);
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
