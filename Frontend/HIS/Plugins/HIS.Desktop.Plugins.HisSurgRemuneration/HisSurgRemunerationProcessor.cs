using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;
using Inventec.Common.Logging;
using HIS.Desktop.Plugins.HisSurgRemuneration.HisSurgRemuneration;
namespace HIS.Desktop.Plugins.HisSurgRemuneration
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.HisSurgRemuneration",
        "Danh sách gói thầu",
        "Bussiness",
        4,
        "pttt.png",
        "D",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true
        )
        ]
    public class HisSurgRemunerationProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisSurgRemunerationProcessor()
        {
            param = new CommonParam();
        }
        public HisSurgRemunerationProcessor(CommonParam commonparam)
        {
            if (commonparam != null)
            {
                param = (commonparam != null ? commonparam : new CommonParam());
            }
        }
        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object Reuslt = null;
            try
            {
                Module module = null;
                if (args != null && args.Count() > 0)
                {
                    for (int i = 0; i < args.Count(); i++)
                    {
                        if (args[i] is Module)
                        {
                            module = (Module)args[i];
                        }
                    }
                    Reuslt = new FrmHisSurgRemuneration(module);
                }
                
            }
            catch (Exception ex)
            {

                LogSystem.Error(ex);
            }
            return Reuslt;
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
                result = false;
                LogSession.Error(ex);
            }
            return result;
        }
    }
}
