using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.AnticipateUpdate.AnticipateUpdate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.AnticipateUpdate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.AnticipateUpdate",
       "Tạo dự trù",
       "Common",
       16,
       "du-tru.png",
       "D",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]

    public class AnticipateUpdateProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public AnticipateUpdateProcessor()
        {
            param = new CommonParam();
        }
        public AnticipateUpdateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IAnticipateUpdate behavior = AnticipateUpdateFactory.MakeIAnticipateUpdate(param, args);
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
