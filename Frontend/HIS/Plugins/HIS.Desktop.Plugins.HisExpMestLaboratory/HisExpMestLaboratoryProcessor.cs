using HIS.Desktop.Plugins.HisExpMestLaboratory.HisExpMestLaboratory;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisExpMestLaboratory
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
          "HIS.Desktop.Plugins.HisExpMestLaboratory",
          "Xuất hóa chất xét nghiệm",
          "Common",
          23,
          "",
          "A",
          Module.MODULE_TYPE_ID__FORM,
          true,
          true
          )
       ]
    public class HisExpMestLaboratoryProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisExpMestLaboratoryProcessor()
        {
            param = new CommonParam();
        }
        public HisExpMestLaboratoryProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IHisExpMestLaboratory behavior = HisExpMestLaboratoryFactory.MakeIHisExpMestLaboratory(param, args);
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
