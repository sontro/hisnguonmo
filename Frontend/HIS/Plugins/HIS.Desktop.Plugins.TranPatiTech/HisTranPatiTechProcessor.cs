using HIS.Desktop.Plugins.TranPatiTech.TranPatiTech;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TranPatiTech
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
   "HIS.Desktop.Plugins.TranPatiTech",
   "Lý do chuyển tuyến chuyên môn",
   "Bussiness",
   4,
   "",
   "",
   Module.MODULE_TYPE_ID__FORM,
   true,
   true)
]
    public class HisTranPatiTechProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisTranPatiTechProcessor()
        {
            param = new CommonParam();
        }
        public HisTranPatiTechProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IHisTranPatiTech behavior = TranPatiTechFactory.MakeIControl(param, args);
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
