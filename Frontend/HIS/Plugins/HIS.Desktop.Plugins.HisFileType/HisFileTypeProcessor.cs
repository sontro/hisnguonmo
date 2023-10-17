using HIS.Desktop.Plugins.HisFileType.HisFileType;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisFileType
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
            "HIS.Desktop.Plugins.HisFileType",
            "Danh mục",
            "Bussiness",
            4,
            "file-type.png",
            "A",
            Module.MODULE_TYPE_ID__FORM,
            true,
            true)]
    public class HisFileTypeProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisFileTypeProcessor()
        {
            param = new CommonParam();
        }
        public HisFileTypeProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IHisFileType behavior = HisFileTypeFactory.MakeIControl(param, args);
                result = behavior != null ? (object)(behavior.Run()) : null;
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
