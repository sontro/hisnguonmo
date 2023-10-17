using HIS.Desktop.Plugins.ContactDrugstore.ContactDrugstore;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ContactDrugstore
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ContactDrugstore",
        "Liên thông nhà thuốc",
        "Common",
        69,
        "ContactDrugstore.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class ContactDrugstoreProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ContactDrugstoreProcessor()
        {
            param = new CommonParam();
        }
        public ContactDrugstoreProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IContactDrugstore behavior = ContactDrugstoreFactory.MakeIContactDrugstore(param, args);
                result = (behavior != null) ? behavior.Run() : null;
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
