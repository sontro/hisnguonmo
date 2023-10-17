using HIS.Desktop.Plugins.TreatmentFundList.TreatmentList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFundList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.TreatmentFundList",
        "Quản lý hồ sơ có Quỹ cùng chi trả",
        "Common",
        68,
        "tai-chinh.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class TreatmentListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TreatmentListProcessor()
        {
            param = new CommonParam();
        }
        public TreatmentListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITreatmentList behavior = TreatmentListFactory.MakeITreatmentList(param, args);
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
