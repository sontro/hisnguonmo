using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.AnticipateCreate.AnticipateCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.AnticipateCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.AnticipateCreate",
       "Tạo dự trù",
       "Common",
       16,
       "du-tru.png",
       "D",
       Module.MODULE_TYPE_ID__UC,
       true,
       true)
    ]

    public class AnticipateCreateProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public AnticipateCreateProcessor()
        {
            param = new CommonParam();
        }
        public AnticipateCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IAnticipateCreate behavior = AnticipateCreateFactory.MakeIAnticipateCreate(param, args);
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
