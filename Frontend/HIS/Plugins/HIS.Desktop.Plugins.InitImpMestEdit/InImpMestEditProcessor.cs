using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InImpMestEdit
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
          "HIS.Desktop.Plugins.InitImpMestEdit",
          "Sửa phiếu nhập đầu kỳ",
          "Common",
          16,
          "impMestCreate.png",
          "E",
          Module.MODULE_TYPE_ID__FORM,
          true,
          true)
       ]
    public class InImpMestEditProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public InImpMestEditProcessor()
        {
            param = new CommonParam();
        }
        public InImpMestEditProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                InImpMestEdit.IInImpMestEdit behavior = InImpMestEdit.InImpMestEditFactory.MakeIInImpMestEdit(param, args);
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
