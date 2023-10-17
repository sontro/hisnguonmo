using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.MaterialList.MaterialList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.MaterialList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
            "HIS.Desktop.Plugins.MaterialList",
            "Danh sách vật tư",
            "Common",
            16,
            "vat-tu.png",
            "E",
            Module.MODULE_TYPE_ID__UC,
            true,
            true)
       ]

    public class MaterialListProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MaterialListProcessor()
        {
            param = new CommonParam();
        }
        public MaterialListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IMaterialList behavior = MaterialListFactory.MakeIMaterialList(param, args);
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
