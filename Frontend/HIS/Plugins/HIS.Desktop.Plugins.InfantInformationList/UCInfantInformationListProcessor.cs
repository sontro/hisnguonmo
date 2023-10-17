using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.InfantInformationList
{
    class UCInfantInformationListProcessor
    {
        [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.InfantInformationList",
           "",
           "",
           0,
           "",
           "",
           Module.MODULE_TYPE_ID__UC,
           true,
           true)
        ]
        public class UCInfantInformationListQProcessor : ModuleBase, IDesktopRoot
        {
            CommonParam param;
            public UCInfantInformationListQProcessor()
            {
                param = new CommonParam();
            }
            public UCInfantInformationListQProcessor(CommonParam paramBusiness)
            {
                param = (paramBusiness != null ? paramBusiness : new CommonParam());
            }

            public object Run(object[] args)
            {
                object result = null;
                try
                {
                    InfantInformationList.IInfantInformationList behavior = InfantInformationList.InfantInformationListFactory.MakeIInfantInformationList(param, args);
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
