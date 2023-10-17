using HIS.Desktop.Plugins.DiscountSereServ.DiscountSereServ;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DiscountSereServ
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.DiscountSereServ",
        "Cập nhật chiết khấu",
        "Common",
        67,
        "discountSereServ.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class DiscountSereServProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public DiscountSereServProcessor()
        {
            param = new CommonParam();
        }
        public DiscountSereServProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IDiscountSereServ behavior = DiscountSereServFactory.MakeIDiscountSereServ(param, args);
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
