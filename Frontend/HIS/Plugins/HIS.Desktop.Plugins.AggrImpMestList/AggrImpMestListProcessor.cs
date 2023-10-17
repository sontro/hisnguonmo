using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AggrImpMestList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
          "HIS.Desktop.Plugins.AggrImpMestList",
          "Danh sách phiếu trả",
          "Common",
          16,
          "AggrImpMest_32x32.png",
          "C",
          Module.MODULE_TYPE_ID__UC,
          true,
          true)
       ]

    class AggrImpMestListProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public AggrImpMestListProcessor()
        {
            param = new CommonParam();
        }
        public AggrImpMestListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                AggrImpMestList.IAggrImpMestList behavior = AggrImpMestList.AggrImpMestListFactory.MakeIAggrImpMestList(param, args);
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
