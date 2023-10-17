using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.HisBloodTypeVolume
{
    class HisBloodTypeVolumeProcessor
    {
        [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.HisBloodTypeVolume",
           "Thiết lập máu – dung tích hiến máu",
           "",
           0,
           "",
           "",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
        ]
        public class HisBloodTypeVolumeQProcessor : ModuleBase, IDesktopRoot
        {
            CommonParam param;
            public HisBloodTypeVolumeQProcessor()
            {
                param = new CommonParam();
            }
            public HisBloodTypeVolumeQProcessor(CommonParam paramBusiness)
            {
                param = (paramBusiness != null ? paramBusiness : new CommonParam());
            }

            public object Run(object[] args)
            {
                object result = null;
                try
                {
                    HisBloodTypeVolume.IHisBloodTypeVolume behavior = HisBloodTypeVolume.HisBloodTypeVolumeFactory.MakeIHisBloodTypeVolume(param, args);
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
