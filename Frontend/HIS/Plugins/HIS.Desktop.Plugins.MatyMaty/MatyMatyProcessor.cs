using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MatyMaty
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
     "HIS.Desktop.Plugins.MatyMaty",
     "Danh mục",
     "Bussiness",
     4,
     "showproduct_32x32.png",
     "A",
     Module.MODULE_TYPE_ID__FORM,
     true,
     true)
  ]
    public class MatyMatyProcessor : ModuleBase, IDesktopRoot
    {

        CommonParam param;

        public MatyMatyProcessor()
        {
            param = new CommonParam();
        }
        public MatyMatyProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                List<HIS_MATY_MATY> LisMatyMaty = null;
                long materialTypeId = 0;
                if (args.GetType() == typeof(object[]))
                {
                    if (args != null && args.Count() > 0)
                    {
                        for (int i = 0; i < args.Count(); i++)
                        {
                            if (args[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)args[i];
                            }
                            else if (args[i] is List<HIS_MATY_MATY>)
                            {
                                LisMatyMaty = (List<HIS_MATY_MATY>)args[i];
                            }
                            else if (args[i] is long)
                            {
                                materialTypeId = (long)args[i];
                            }
                        }
                    }
                }

                if (materialTypeId > 0)
                    result = new frmMatyMaty(moduleData, materialTypeId, LisMatyMaty);
                else
                    result = new frmMatyMaty(moduleData);
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
