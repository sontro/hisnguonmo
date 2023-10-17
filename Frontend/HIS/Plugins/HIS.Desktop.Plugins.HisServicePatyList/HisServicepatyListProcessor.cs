using HIS.Desktop.Plugins.HisServicePatyList.HisServicePatyList;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;

namespace HIS.Desktop.Plugins.HisServicePatyList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.HisServicePatyList",
        "Thiết lập chính sách giá dịch vụ",
        "Common",
        62,
        "tai-chinh.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class HisServicePatyListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisServicePatyListProcessor()
        {
            param = new CommonParam();
        }
        public HisServicePatyListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IHisServicePatyList behavior = HisServicePatyListFactory.MakeIHisServicePatyList(param, args);
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