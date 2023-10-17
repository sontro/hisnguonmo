using HIS.Desktop.Plugins.ExpBloodChmsCreate.ExpBloodChmsCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpBloodChmsCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ExpBloodChmsCreate",
        "Tạo xuất chuyển kho máu",
        "Common",
        25,
        "expMestCreate.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class ExpBloodChmsCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpBloodChmsCreateProcessor()
        {
            param = new CommonParam();
        }
        public ExpBloodChmsCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IExpBloodChmsCreate behavior = ExpBloodChmsCreateFactory.MakeIExpMestChmsCreate(param, args);
                result = (behavior != null) ? behavior.Run() : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
