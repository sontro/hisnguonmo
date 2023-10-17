using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Plugins.ReportCreate.ReportCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Common.Core;
using Inventec.Desktop.Common.Modules;

namespace Inventec.Desktop.Plugins.ReportCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.ReportCreate",
       "Tạo báo cáo",
       "Common",
       13,
       "meeting_32x32.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true
       )
    ]
    public class ReportCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ReportCreateProcessor()        
        {
            param = new CommonParam();
        }
        public ReportCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IReportCreate behavior = ReportCreateFactory.MakeIReportCreate(param, null);
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
