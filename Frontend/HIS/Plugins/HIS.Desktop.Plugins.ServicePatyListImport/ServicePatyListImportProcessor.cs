using HIS.Desktop.Plugins.ServicePatyListImport.ServicePatyListImport;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServicePatyListImport
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ServicePatyListImport",
        "Danh sách file Import",
        "Common",
        62,
        "bidList.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class ServicePatyListImportProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ServicePatyListImportProcessor()
        {
            param = new CommonParam();
        }
        public ServicePatyListImportProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IServicePatyListImport behavior = ServicePatyListImportFactory.MakeIServicePatyListImport(param, args);
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
            return false;
        }
    }
}
