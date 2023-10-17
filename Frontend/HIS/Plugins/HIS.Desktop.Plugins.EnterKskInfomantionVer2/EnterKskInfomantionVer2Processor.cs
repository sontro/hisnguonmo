using Inventec.Core;
using HIS.Desktop.Common;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Common.Modules;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.EnterKskInfomantionVer2.EnterKskInfomantionVer2;

namespace HIS.Desktop.Plugins.EnterKskInfomantionVer2
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.EnterKskInfomantionVer2",
       "Thông tin khám sức khỏe Ver2",
       "Common",
       14,
       "kham-suc-khoe.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true
       )
    ]
    public class EnterKskInfomantionVer2Processor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public EnterKskInfomantionVer2Processor()
        {
            param = new CommonParam();
        }
        public EnterKskInfomantionVer2Processor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IEnterKskInfomantionVer2 behavior = EnterKskInfomantionVer2Factory.MakeIControl(param, args);
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
