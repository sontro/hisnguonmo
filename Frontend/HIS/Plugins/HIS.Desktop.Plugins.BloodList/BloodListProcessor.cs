using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.BloodList.BloodList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.BloodList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.BloodList",
           "Danh sách máu",
           "Common",
           16,
           "thuoc.png",
           "E",
           Module.MODULE_TYPE_ID__UC,
           true,
           true)
    ]
    public class BloodListProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public BloodListProcessor()
        {
            param = new CommonParam();
        }
        public BloodListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IBloodList behavior = BloodListFactory.MakeIBloodList(param, args);
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
