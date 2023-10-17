using HIS.Desktop.Plugins.HisSereServPtttTemp.SereServPtttTemp;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisSereServPtttTemp
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.HisSereServPtttTemp",
        "Danh sách mẫu PTTT",
        "Common",
        16,
        "dich-vu.png",
        "E",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)
    ]
    public class SereServPtttTempProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public SereServPtttTempProcessor()
        {
            param = new CommonParam();
        }
        public SereServPtttTempProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                ISereServPtttTemp behavior = SereServPtttTempFactory.MakeISereServPtttTemp(param, args);
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
