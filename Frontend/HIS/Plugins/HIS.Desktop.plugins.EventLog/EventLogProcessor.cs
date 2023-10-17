using Inventec.Core;
using Inventec.Desktop.Common.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.EventLog
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.EventLog",
       "Lịch sử sử dụng",
       "Common",
       40,
       "historyitem_32x32.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class EventLogProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public EventLogProcessor()
        {
            param = new CommonParam();
        }
        public EventLogProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        /// <summary>
        /// </summary>
        /// <param name="args">Dau vao co 3 tham so: SdaConsumer, NumPageSize, Inventec.UC.EventLogControl.ProcessHasException</param>
        /// <returns></returns>
        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IEventLog behavior = EventLogFactory.MakeICrateType(param, args);
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
