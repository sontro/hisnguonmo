using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ExpMestHcsCreate.ExpMestHcsCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.ExpMestHcsCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
          "HIS.Desktop.Plugins.ExpMestHcsCreate",
          "Danh sách xuất",
          "Common",
          16,
          "ExpMest_32x32.png",
          "E",
          Module.MODULE_TYPE_ID__UC,
          true,
          true)
       ]

    public class ExpMestHcsCreateProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpMestHcsCreateProcessor()
        {
            param = new CommonParam();
        }
        public ExpMestHcsCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IExpMestHcsCreate behavior = ExpMestHcsCreateFactory.MakeIExpMestHcsCreate(param, args);
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
