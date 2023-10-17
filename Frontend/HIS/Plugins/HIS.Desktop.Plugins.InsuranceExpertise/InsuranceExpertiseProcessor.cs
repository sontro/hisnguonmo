using Inventec.Core;
using Inventec.Desktop.Common;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.Plugins.InsuranceExpertise.InsuranceExpertise;

namespace HIS.Desktop.Plugins.InsuranceExpertise
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.InsuranceExpertise",
       "Giám định BHYT",
       "Common",
       4,
       "bhxh_32.jpg",
       "A",
       Module.MODULE_TYPE_ID__UC,
       true,
       true)
    ]
    public class InsuranceExpertiseProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public InsuranceExpertiseProcessor()
        {
            param = new CommonParam();
        }
        public InsuranceExpertiseProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IInsuranceExpertise behavior = InsuranceExpertiseFactory.MakeIInsuranceExpertise(param, args);
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
