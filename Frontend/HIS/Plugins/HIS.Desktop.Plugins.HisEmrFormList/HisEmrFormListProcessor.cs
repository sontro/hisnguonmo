using HIS.Desktop.Plugins.HisEmrFormList.HisEmrFormList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisEmrFormList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.HisEmrFormList",
           "Thiết lập phiếu vỏ bệnh án",
           "popup",
           4,
           "ThietLapPhieuVoBenhAn.png",
           "A",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
        ]
    public class HisEmrFormListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisEmrFormListProcessor()
        {
            param = new CommonParam();
        }
        public HisEmrFormListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IHisEmrFormList behavior = HisEmrFormListFactory.MakeIControl(param, args);
                result = behavior != null ? (object)(behavior.Run()) : null;
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
