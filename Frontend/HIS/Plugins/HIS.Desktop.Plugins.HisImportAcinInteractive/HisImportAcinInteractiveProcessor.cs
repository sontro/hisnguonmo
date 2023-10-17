using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImportAcinInteractive
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.HisImportAcinInteractive",
           "Nhập khẩu tương tác thuốc",
           "Common",
           16,
           "xlsx.png",
           "E",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
       ]
    public class HisImportAcinInteractiveProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisImportAcinInteractiveProcessor()
        {
            param = new CommonParam();
        }

        public HisImportAcinInteractiveProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                HisImportAcinInteractive.IHisImportAcinInteractive behavior = HisImportAcinInteractive.HisImportAcinInteractiveFactory.MakeBehavior(param, args);
                result = behavior != null ? (behavior.Run()) : null;
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
