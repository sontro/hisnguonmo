using Inventec.Core;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.SaroExro;
using HIS.Desktop.Plugins.SaroExro.SaroExro;

namespace HIS.Desktop.Plugins.SaroExro
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.SaroExro",
        "Thiết lập phòng xử lý phòng lấy mẫu",
        "Common",
        62,
        "phong-lay-mau.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class SaroExroProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public SaroExroProcessor()
        {
            param = new CommonParam();
        }
        public SaroExroProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ISaroExro behavior = SaroExroFactory.MakeISaroExro(param, args);
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
