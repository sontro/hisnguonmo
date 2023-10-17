using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;

namespace EMR.Desktop.Plugins.EmrBusiness
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "EMR.Desktop.Plugins.EmrBusiness",
        "Danh mục",
        "Bussiness",
        4,
        "ky.png",
        "D",
        Module.MODULE_TYPE_ID__UC,
        true,
        true
        )
    ]
    public class EmrBusinessProcessors : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public EmrBusinessProcessors()
        {
            param = new CommonParam();
        }
        public EmrBusinessProcessors(CommonParam commonparam)
        {
            param = (commonparam != null ? commonparam : new CommonParam());
        }
        public object Run(object[] args)
        {
            object result = null;
            try
            {
                Module moduledata = null;
                if (args != null && args.Count() > 0)
                {
                    for (int i = 0; i < args.Count(); i++)
                    {
                        if (args[i] is Module)
                        {
                            moduledata = (Module)args[i];
                        }
                    }
                    result = new UC_EmrBusiness(moduledata);
                }
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