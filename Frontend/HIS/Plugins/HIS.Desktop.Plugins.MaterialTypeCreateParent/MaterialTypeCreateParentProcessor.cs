using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using HIS.Desktop.Plugins.MaterialTypeCreateParent.MaterialTypeCreateParent;

namespace HIS.Desktop.Plugins.MaterialTypeCreateParent
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
   "HIS.Desktop.Plugins.MaterialTypeCreateParent",
   "Danh mục",
   "Bussiness",
   4,
   "vat-tu.png",
   "A",
   Module.MODULE_TYPE_ID__FORM,
   true,
   true)
]
    public class MaterialTypeCreateParentProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MaterialTypeCreateParentProcessor()
        {
            param = new CommonParam();
        }
        public MaterialTypeCreateParentProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IMaterialTypeCreateParent behavior = MaterialTypeCreateParentFactory.MakeIControl(param, args);
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


