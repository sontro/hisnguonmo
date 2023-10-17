using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using LIS.Desktop.Plugins.LisBacteriumFamily.LisBacteriumFamily;
using Inventec.Common.Logging;

namespace LIS.Desktop.Plugins.LisBacteriumFamily
{
    public class LisBacteriumFamilyProcessor
    {
        [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "LIS.Desktop.Plugins.LisBacteriumFamily",
       "BHYT",
       "Bussiness",
       4,
       "bhyt.png",
       "D",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true
       )
       ]
        public class HisBHYTParamProcessors : ModuleBase, IDesktopRoot
        {
            CommonParam param;
            public HisBHYTParamProcessors()
            {
                param = new CommonParam();
            }
            public HisBHYTParamProcessors(CommonParam commonparam)
            {
                param = (commonparam != null ? commonparam : new CommonParam());
            }
            public object Run(object[] args)
            {
                CommonParam param = new CommonParam();
                object Resutl = null;
                try
                {
                    Module moduleData = null;
                    if (args != null && args.Count() > 0)
                    {
                        for (int i = 0; i < args.Count(); i++)
                        {
                            if (args[i] is Module)
                            {
                                moduleData = (Module)args[i];
                            }
                        }
                        Resutl = new frmLisBacteriumFamily(moduleData);
                    }
                }
                catch (Exception ex)
                {
                    LogSystem.Error(ex);
                    Resutl = null;
                }
                return Resutl;
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

                    LogSystem.Error(ex);
                    success = false;
                }
                return success;
            }
        }
    }
}
