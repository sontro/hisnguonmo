
using HIS.Desktop.Plugins.SarUserReportTypeList.SarUserReportTypeList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SarUserReportTypeList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.SarUserReportTypeList",
        "Thiết lập Tài khoản - Loại báo cáo",
        "Common",
        68,
        "reading_32x32.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class SarUserReportTypeListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public SarUserReportTypeListProcessor()
        {
            param = new CommonParam();
        }
        public SarUserReportTypeListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ISarUserReportTypeList behavior = SarUserReportTypeListFactory.MakeISarUserReportTypeList(param, args);
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
