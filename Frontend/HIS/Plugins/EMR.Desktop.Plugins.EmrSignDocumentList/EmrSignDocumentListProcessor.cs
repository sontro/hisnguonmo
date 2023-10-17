using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.EmrSignDocumentList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
    "EMR.Desktop.Plugins.EmrSignDocumentList",
    "Văn bản tôi ký",
    "Common",
    62,
    "",
    "A",
    Module.MODULE_TYPE_ID__UC,
    true,
    true)]
    public class EmrSignDocumentListProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public EmrSignDocumentListProcessor()
        {
            param = new CommonParam();
        }
        public EmrSignDocumentListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                EmrSignDocumentList.IEmrSignDocumentList behavior = EmrSignDocumentList.EmrSignDocumentListFactory.MakeIEmrSignDocumentList(param, args);
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
