using Inventec.Core;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.LisSampleUpdate;
using HIS.Desktop.Plugins.LisSampleUpdate.ConnectionTest;

namespace HIS.Desktop.Plugins.LisSampleUpdate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.LisSampleUpdate",
        "Sửa thông tin mẫu bệnh phẩm",
        "Common",
        62,
        "technology_32x32.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class LisSampleUpdateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public LisSampleUpdateProcessor()
        {
            param = new CommonParam();
        }
        public LisSampleUpdateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IConnectionTest behavior = ConnectionTestFactory.MakeIConnectionTest(param, args);
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
