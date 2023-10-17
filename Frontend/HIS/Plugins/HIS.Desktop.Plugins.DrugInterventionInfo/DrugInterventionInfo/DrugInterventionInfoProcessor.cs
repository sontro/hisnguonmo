using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DrugInterventionInfo
{
    class DrugInterventionInfoProcessor
    {
        [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.DrugInterventionInfo",
           "Thông tin can thiệp dược",
           "",
           1,
           "",
           "",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
        ]
        public class DrugInterventionInfoProcessorA : ModuleBase, IDesktopRoot
        {
            CommonParam param;
            public DrugInterventionInfoProcessorA()
            {
                param = new CommonParam();
            }
            public DrugInterventionInfoProcessorA(CommonParam paramBusiness)
            {
                param = (paramBusiness != null ? paramBusiness : new CommonParam());
            }

            public object Run(object[] args)
            {
                object result = null;
                try
                {
                    IDrugInterventionInfo behavior = DrugInterventionInfoFactory.MakeIControl(param, args);
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
