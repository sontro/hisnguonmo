using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;
using HIS.Desktop.Plugins.HisDebateTemp.HisDebateTemp;

namespace HIS.Desktop.Plugins.HisDebateTemp
{
    
   [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.HisDebateTemp",
        "Danh mục",
        "Bussiness",
        4,
        "",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true
        )
    ]
    public class HisDebateTempProcessors : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisDebateTempProcessors()
        {
            param = new CommonParam();
        }
        public HisDebateTempProcessors(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IHisDebateTemp behavior = HisDebateTempFactory.MakeIControl(param, args);
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
            return false;
        }
    }
}

