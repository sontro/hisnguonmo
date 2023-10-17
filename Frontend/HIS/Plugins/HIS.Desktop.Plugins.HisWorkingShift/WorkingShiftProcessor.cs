using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using HIS.Desktop.Plugins.HisWorkingShift.HisWorkingShift;

namespace HIS.Desktop.Plugins.HisWorkingShift
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
  "HIS.Desktop.Plugins.HisWorkingShift",
  "Danh mục",
  "Bussiness",
  4,
  "thoi-gian.png",
  "A",
  Module.MODULE_TYPE_ID__FORM,
  true,
  true)
]
    public class WorkingShiftProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public WorkingShiftProcessor()
        {
            param = new CommonParam();
        }
        public WorkingShiftProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IWorkingShift behavior = WorkingShiftFactory.MakeIControl(param, args);
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
