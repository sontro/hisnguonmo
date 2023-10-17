using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using Inventec.Core;

namespace HIS.Desktop.Plugins.HisImportPtttTable
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.HisImportPtttTable",
        "Import",
        "Common",
        14,
        "giuong-benh.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
    true,
    true
    )
    ]
    public class HisImportPtttTableProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisImportPtttTableProcessor()
        {
            param = new CommonParam();
        }
        public HisImportPtttTableProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());

        }
        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
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
                    result = new frmHisImportPtttTable(moduleData);
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
