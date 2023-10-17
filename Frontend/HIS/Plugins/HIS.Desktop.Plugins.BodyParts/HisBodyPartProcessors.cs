using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;
using HIS.Desktop.Plugins.BodyParts;

namespace HIS.Desktop.Plugins.BodyParts
{
    
        [ExtensionOf(typeof(DesktopRootExtensionPoint),
    "HIS.Desktop.Plugins.BodyParts",
    "Danh mục",
    "Bussiness",
    4,
    "",
    "A",
    Module.MODULE_TYPE_ID__FORM,
    true,
    true)
 ]
        public class BodyPartsProcessor : ModuleBase, IDesktopRoot
        {
            CommonParam param;
            public BodyPartsProcessor()
            {
                param = new CommonParam();
            }
            public BodyPartsProcessor(CommonParam paramBusiness)
            {
                param = (paramBusiness != null ? paramBusiness : new CommonParam());
            }

            public object Run(object[] args)
            {
                object result = null;
                try
                {
                    IBodyParts behavior = BodyPartsFactory.MakeIControl(param, args);
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

