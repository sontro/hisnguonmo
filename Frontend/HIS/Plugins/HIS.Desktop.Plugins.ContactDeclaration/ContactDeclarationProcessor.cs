using HIS.Desktop.Plugins.ContactDeclaration.ContactDeclaration;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ContactDeclaration
{
    class HisContactDeclarationProcessor
    {
        [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.ContactDeclaration",
           "Khai báo thông tin tiếp xúc",
           "Bussiness",
           4,
           "khai_bao_tiep_xuc.png",
           "A",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
        ]

        public class ContactDeclarationProcessor : ModuleBase, IDesktopRoot
        {
            CommonParam param;
            public ContactDeclarationProcessor()
            {
                param = new CommonParam();
            }
            public ContactDeclarationProcessor(CommonParam paramBusiness)
            {
                param = (paramBusiness != null ? paramBusiness : new CommonParam());
            }

            public object Run(object[] args)
            {
                object result = null;
                try
                {
                    IContactDeclaration behavior = ContactDeclarationFactory.MakeIControl(param, args);
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
