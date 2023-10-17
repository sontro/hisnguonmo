using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.LisDeliveryNoteList
{
    class LisDeliveryNoteListProcessor
    {
        [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.LisDeliveryNoteList",
           "Danh sách phiếu giao bệnh phẩm",
           "",
           0,
           "",
           "",
           Module.MODULE_TYPE_ID__UC,
           true,
           true)
        ]
        public class LisDeliveryNoteListQProcessor : ModuleBase, IDesktopRoot
        {
            CommonParam param;
            public LisDeliveryNoteListQProcessor()
            {
                param = new CommonParam();
            }
            public LisDeliveryNoteListQProcessor(CommonParam paramBusiness)
            {
                param = (paramBusiness != null ? paramBusiness : new CommonParam());
            }

            public object Run(object[] args)
            {
                object result = null;
                try
                {
                    LisDeliveryNoteList.ILisDeliveryNoteList behavior = LisDeliveryNoteList.LisDeliveryNoteListFactory.MakeILisDeliveryNoteList(param, args);
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
}
