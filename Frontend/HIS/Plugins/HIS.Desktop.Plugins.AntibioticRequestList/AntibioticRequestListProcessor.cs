using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.AntibioticRequestList.AntibioticRequestList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.AntibioticRequestList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
          "HIS.Desktop.Plugins.AntibioticRequestList",
          "Danh sách xuất",
          "Common",
          16,
          "xuat-kho.png",
          "E",
          Module.MODULE_TYPE_ID__UC,
          true,
          true)
       ]

    public class AntibioticRequestListProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public AntibioticRequestListProcessor()
        {
            param = new CommonParam();
        }
        public AntibioticRequestListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IAntibioticRequestList behavior = AntibioticRequestListFactory.MakeIAntibioticRequestList(param, args);
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
