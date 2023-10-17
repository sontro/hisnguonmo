using Inventec.Core;
using Inventec.Desktop.Common;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using SAR.Desktop.Plugins.SarReportTypeGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.Common;

namespace SAR.Desktop.Plugins.SarReportTypeGroup
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "SAR.Desktop.Plugins.SarReportTypeGroup",
       "Danh mục",
       "Bussiness",
       4,
       "quy-tai-chinh.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class SarReportTypeGroupProcessor : ModuleBase, IDesktopRoot
    {
		CommonParam param;
		public SarReportTypeGroupProcessor()
        {
            param = new CommonParam();
        }
        public SarReportTypeGroupProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }        

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                ISarReportTypeGroup behavior = SarReportTypeGroupFactory.MakeIControl(param, args);
                result = behavior != null ? (object)(behavior.Run()) : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
		
		/// <summary>
        /// Ham tra ve trang thai cua module la enable hay disable
        /// Ghi de gia tri khac theo nghiep vu tung module
        /// </summary>
        /// <returns>true/false</returns>
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
