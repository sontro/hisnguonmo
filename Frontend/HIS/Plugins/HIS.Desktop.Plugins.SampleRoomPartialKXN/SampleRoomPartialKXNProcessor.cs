using HIS.Desktop.Plugins.SampleRoomPartialKXN.SampleRoomPartialKXN;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SampleRoomPartialKXN
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.SampleRoomPartialKXN",
        "Phòng xử lý lấy mẫu",
        "Common",
        27,
        "version_32x32.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    class SampleRoomPartialKXNProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public SampleRoomPartialKXNProcessor()
        {
            param = new CommonParam();
        }
        public SampleRoomPartialKXNProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ISampleRoomPartialKXN behavior = SampleRoomPartialKXNFactory.MakeISampleRoomPartialKXN(param, args);
                result = (behavior != null) ? behavior.Run() : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
