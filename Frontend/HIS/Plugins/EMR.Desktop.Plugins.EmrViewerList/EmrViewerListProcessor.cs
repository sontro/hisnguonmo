using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.EmrViewerList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
    "EMR.Desktop.Plugins.EmrViewerList",
    "Danh sách xem BAĐT(*)",
    "Common",
    62,
    "",
    "A",
    Module.MODULE_TYPE_ID__UC,
    true,
    true)]
    public class EmrViewerListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public EmrViewerListProcessor()
        {
            param = new CommonParam();
        }
        public EmrViewerListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                EmrViewerList.IEmrViewerList behavior = EmrViewerList.EmrViewerListFactory.MakeIEmrViewerList(param, args);
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
