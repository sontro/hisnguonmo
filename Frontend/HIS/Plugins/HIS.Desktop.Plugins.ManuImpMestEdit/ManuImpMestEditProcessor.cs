using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ManuImpMestEdit.ManuImpMestEdit;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.ManuImpMestEdit
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
          "HIS.Desktop.Plugins.ManuImpMestEdit",
          "Sửa thông tin nhập từ nhà cung cấp",
          "Common",
          62,
          "ManuImpMest_32x32.png",
          "A",
          Module.MODULE_TYPE_ID__FORM,
          true,
          true)]

    public class ManuImpMestEditProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ManuImpMestEditProcessor()
        {
            param = new CommonParam();
        }
        public ManuImpMestEditProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IManuImpMestEdit behavior = ManuImpMestEditFactory.MakeIManuImpMestEdit(param, args);
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
            return false;
        }
    }
}
