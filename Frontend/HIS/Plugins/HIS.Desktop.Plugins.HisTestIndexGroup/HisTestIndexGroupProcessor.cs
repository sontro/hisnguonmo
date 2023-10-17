using HIS.Desktop.Plugins.HisTestIndexGroup.HisTestIndexGroup;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisTestIndexGroup
{
    class HisTestIndexGroupProcessor
    {
        [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.HisTestIndexGroup",
           "Nhóm chỉ số xét nghiệm",
           "Danh mục",
           4,
           "chi-so.png",
           "A",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
        ]
        public class DocumentBookProcessor : ModuleBase, IDesktopRoot
        {
            CommonParam param;
            public DocumentBookProcessor()
            {
                param = new CommonParam();
            }
            public DocumentBookProcessor(CommonParam paramBusiness)
            {
                param = (paramBusiness != null ? paramBusiness : new CommonParam());
            }

            public object Run(object[] args)
            {
                object result = null;
                try
                {
                    IHisTestIndexGroup behavior = HisTestIndexGroupFactory.MakeIControl(param, args);
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
