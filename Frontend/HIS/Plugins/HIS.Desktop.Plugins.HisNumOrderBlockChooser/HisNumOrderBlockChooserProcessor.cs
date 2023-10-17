using HIS.Desktop.Plugins.HisNumOrderBlockChooser.HisNumOrderBlockChooser;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisNumOrderBlockChooser
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.HisNumOrderBlockChooser",
       "Chọn khung giờ khám",
       "Common",
       14,
       "",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true
       )
    ]
    public class HisNumOrderBlockChooserProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisNumOrderBlockChooserProcessor()
        {
            param = new CommonParam();
        }

        public HisNumOrderBlockChooserProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IHisNumOrderBlockChooser behavior = HisNumOrderBlockChooserFactory.MakeIHisNumOrderBlockChooser(param, args);
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
