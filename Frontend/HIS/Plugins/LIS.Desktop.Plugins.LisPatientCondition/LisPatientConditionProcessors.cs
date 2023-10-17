using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;
using LIS.Desktop.Plugins.LisPatientCondition;

namespace LIS.Desktop.Plugins.LisPatientCondition
{

    [ExtensionOf(typeof(DesktopRootExtensionPoint),
"LIS.Desktop.Plugins.LisPatientCondition",
"Danh mục",
"Bussiness",
4,
"",
"A",
Module.MODULE_TYPE_ID__FORM,
true,
true)
]
    public class LisPatientConditionProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public LisPatientConditionProcessor()
        {
            param = new CommonParam();
        }
        public LisPatientConditionProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                ILisPatientCondition behavior = LisPatientConditionFactory.MakeIControl(param, args);
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

