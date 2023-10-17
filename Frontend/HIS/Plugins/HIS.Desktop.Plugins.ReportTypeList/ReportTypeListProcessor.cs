using Inventec.Core;
using Inventec.Desktop.Common.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Plugins.ReportTypeList.ReportTypeList;
using System;
using System.Windows.Forms;

namespace Inventec.Desktop.Plugins.ReportTypeList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.ImportSe_Gr",
       "Danh sách loại báo cáo",
       "Common",
       14,//Image index
       "switchrowcolumn_32x32.png",//Image icon name
       "A",
       Module.MODULE_TYPE_ID__UC,
       true,
       true
       )
    ]
    public class ReportTypeListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ReportTypeListProcessor()
        {
            param = new CommonParam();
        }
        public ReportTypeListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IReportTypeList behavior = ReportTypeListFactory.MakeIReportTypeList(param, args);
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
