using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using EMR.Desktop.Plugins.EmrDocumentListAll.EmrDocumentListAll;
namespace EMR.Desktop.Plugins.EmrDocumentListAll
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
      "EMR.Desktop.Plugins.EmrDocumentListAll",
      "Danh sách văn bản",
      "Common",
      16,
      "van-ban.png",
      "D",
      Module.MODULE_TYPE_ID__UC,
      true,
      true)
   ]

    public class EmrDocumentListAllProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public EmrDocumentListAllProcessor()
        {
            param = new CommonParam();
        }
        public EmrDocumentListAllProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IEmrDocumentListAll behavior = EmrDocumentListAllFactory.MakeIHisBidList(param, args);
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
            bool result = true;
              
            return result;
        }
    }
  
}
