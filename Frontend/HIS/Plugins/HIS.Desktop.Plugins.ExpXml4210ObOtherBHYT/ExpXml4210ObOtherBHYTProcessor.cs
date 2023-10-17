using Inventec.Core;
using HIS.Desktop.Common;
using Inventec.Desktop.Core;
using HIS.Desktop.Plugins.ExpXml4210ObOtherBHYT.Run;
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

namespace HIS.Desktop.Plugins.ExpXml4210ObOtherBHYT
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.ExpXml4210ObOtherBHYT",
       "Xuất XML4210 cho đối tượng khác BHYT",
       "Common",
       23,
       "pivot_32x32.png",
       "A",
       Module.MODULE_TYPE_ID__UC,
       true,
       true
       )
    ]
    public class ExpXml4210ObOtherBHYTProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpXml4210ObOtherBHYTProcessor()
        {
            param = new CommonParam();
        }
        public ExpXml4210ObOtherBHYTProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IExpXml4210ObOtherBHYT behavior = ExpXml4210ObOtherBHYTFactory.MakeIExpXml4210ObOtherBHYT(param, args);
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
