using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.MaterialUpdate.MaterialUpdate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.MaterialUpdate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.MaterialUpdate",
       "Sửa thông tin vật tư",
       "Common",
       62,
       "MaterialList.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)]
    public class MaterialUpdatePorcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MaterialUpdatePorcessor()
        {
            param = new CommonParam();
        }
        public MaterialUpdatePorcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IMaterialUpdate behavior = MaterialUpdateFactory.MakeIMaterialUpdate(param, args);
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
            return false;
        }
    }
}
