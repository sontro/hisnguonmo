using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Inventec.Desktop.Common.Modules;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Core;
using HIS.Desktop.Plugins.HisImportEmpUser.ImportEmpUser;
using HIS.Desktop.Plugins.HisImportEmpUser.ImportEmpUser.Run;



namespace HIS.Desktop.Plugins.HisImportEmpUser
{
     [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.HisImportEmpUser",
       "Import",
       "Common",
       14,
       "pivot_32x32.png",
       "A",
       Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__FORM,
       true,
       true
       )
    ]
     public class HisImportEmpUserProcessor : ModuleBase ,  Inventec.Desktop.Core.IDesktopRoot
    {
          CommonParam param;
        public HisImportEmpUserProcessor()
        {
            param = new CommonParam();
        }
        public HisImportEmpUserProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IHisImportEmpUser behavior = HisImportEmpUserFactory.MakeIHisImportEmpUser(param, args);
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
