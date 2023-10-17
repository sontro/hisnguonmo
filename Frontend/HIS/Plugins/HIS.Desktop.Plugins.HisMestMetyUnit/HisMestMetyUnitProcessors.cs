using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using Inventec.Core;
using Inventec.Common.Logging;
using HIS.Desktop.Plugins.HisMestMetyUnit.HisMestMetyUnit;

namespace HIS.Desktop.Plugins.HisMestMetyUnit
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.HisMestMetyUnit",
        "Thiết lập",
        "Bussiness",
        14,
        "thiet-lap.png",
        "D",
         Module.MODULE_TYPE_ID__FORM,
        true,
        true
        )
    ]
    class HisMestMetyUnitProcessors:ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisMestMetyUnitProcessors()
        {
            param = new CommonParam();
        }
        public HisMestMetyUnitProcessors(CommonParam commonparam)
        {
            param = (commonparam != null ? commonparam : new CommonParam());
        }
        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object Result = null;
            try
            {
                Module Module = null;
                if (args != null && args.Count() > 0)
                {
                    for (int i = 0; i < args.Count(); i++)
                    {
                        if (args[i] is Module)
                        {
                            Module = (Module)args[i];
                        }
                    }
                    Result = new frmHisMestMetyUnit(Module);
                }
            }
            catch (Exception ex)
            {

                LogSystem.Error(ex);
                Result = null;
            }
            return Result;
        }
        public override bool IsEnable()
        {
            bool success = false;
            try
            {
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
                LogSystem.Error(ex);
            }
            return success;
        }
    }
}
