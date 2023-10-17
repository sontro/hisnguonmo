using HIS.Desktop.Plugins.OtherImpMestUpdate.OtherImpMestUpdate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.OtherImpMestUpdate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
          "HIS.Desktop.Plugins.OtherImpMestUpdate",
          "Sửa phiếu nhập khác",
          "Common",
          16,
          "impMestCreate.png",
          "E",
          Module.MODULE_TYPE_ID__FORM,
          true,
          true)
       ]
    public class OtherImpMestUpdateProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public OtherImpMestUpdateProcessor()
        {
            param = new CommonParam();
        }
        public OtherImpMestUpdateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IOtherImpMestUpdate behavior = OtherImpMestUpdateFactory.MakeIManuImpMestEdit(param, args);
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
