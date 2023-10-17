using HIS.Desktop.Plugins.HisQcNormation.HisQcNormation;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisQcNormation
{

    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.HisQcNormation",
       "Định mức hóa chất chạy QC",
       "Bussiness",
       4,
       "Dinh_muc.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]

    public class HisQcNormationProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisQcNormationProcessor()
        {
            param = new CommonParam();
        }
        public HisQcNormationProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IHisQcNormation behavior = HisQcNormationFactory.MakeIControl(param, args);
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
