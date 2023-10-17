using SDA.Desktop.Plugins.SdaField;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.Desktop.Plugins.SdaField
{

        [ExtensionOf(typeof(DesktopRootExtensionPoint),
   "SDA.Desktop.Plugins.SdaField",
   "Chọn phòng",
   "Common",
   0,
   "chon-phong.png",
   "A",
   Module.MODULE_TYPE_ID__FORM,
   true,
   true)
    ]

        public class SdaFieldProcessor : ModuleBase, IDesktopRoot
        {
            CommonParam param;
            public SdaFieldProcessor()
            {
                param = new CommonParam();
            }
            public SdaFieldProcessor(CommonParam paramBusiness)
            {
                param = (paramBusiness != null ? paramBusiness : new CommonParam());
            }

            public object Run(object[] args)
            {
                object result = null;
                try
                {
                    ISdaField behavior = SdaFieldFactory.MakeIControl(param, args);
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
                return true;
            }
        }
    }

