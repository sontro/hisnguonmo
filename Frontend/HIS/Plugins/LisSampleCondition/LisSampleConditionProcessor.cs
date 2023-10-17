using Inventec.Core;
using Inventec.Desktop.Common;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.Common;
using LIS.Desktop.Plugins.LisSampleCondition.LisSampleCondition;

namespace LIS.Desktop.Plugins.LisSampleCondition
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
      "LIS.Desktop.Plugins.LisSampleCondition",
      "Xét nghiệm",
      "Bussiness",
      4,
      "mau-benh-pham.png",
      "A",
      Module.MODULE_TYPE_ID__FORM,
      true,
      true)
   ]
    public class LisSampleConditionProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public LisSampleConditionProcessor()
        {
            param = new CommonParam();
        }
        public LisSampleConditionProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                ILisSampleCondition behavior = LisSampleConditionFactory.MakeIControl(param, args);
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
