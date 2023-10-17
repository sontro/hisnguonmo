using HIS.Desktop.Plugins.HisBltyService.HisBltyService;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisBltyService
{
    class HisDocumentBookProcessor
    {
        [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.HisBltyService",
           "Thiết lập kiểm tra truyền máu",
           "menu popup",
           4,
           "ChucNangThietLapKiemTraTruyenMau.png",
           "A",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
        ]

        public class HisBltyServiceProcessor : ModuleBase, IDesktopRoot
        {
            CommonParam param;
            public HisBltyServiceProcessor()
            {
                param = new CommonParam();
            }
            public HisBltyServiceProcessor(CommonParam paramBusiness)
            {
                param = (paramBusiness != null ? paramBusiness : new CommonParam());
            }

            public object Run(object[] args)
            {
                object result = null;
                try
                {
                    IHisBltyService behavior = HisBltyServiceFactory.MakeIControl(param, args);
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
