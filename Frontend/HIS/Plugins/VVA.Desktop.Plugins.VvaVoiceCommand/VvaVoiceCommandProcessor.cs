using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using Vva.Desktop.Plugins.VvaVoiceCommand.VvaVoiceCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVA.Desktop.Plugins.VvaVoiceCommand
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "VVA.Desktop.Plugins.VvaVoiceCommand",
       "Thiết lập lệnh điều khiển",
       "Bussiness",
       4,
       "thiet-lap.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]

    public class VvaVoiceCommandProcessor : ModuleBase, IDesktopRoot
    {
		CommonParam param;
		public VvaVoiceCommandProcessor()
        {
            param = new CommonParam();
        }
        public VvaVoiceCommandProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }        

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IVvaVoiceCommand behavior = VvaVoiceCommandFactory.MakeIControl(param, args);
                result = behavior != null ? (object)(behavior.Run()) : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
		
		/// <summary>
        /// Ham tra ve trang thai cua module la enable hay disable
        /// Ghi de gia tri khac theo nghiep vu tung module
        /// </summary>
        /// <returns>true/false</returns>
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
