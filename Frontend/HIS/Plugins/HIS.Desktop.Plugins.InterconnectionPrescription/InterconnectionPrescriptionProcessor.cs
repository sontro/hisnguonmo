using HIS.Desktop.Plugins.InterconnectionPrescription.InterconnectionPrescription;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InterconnectionPrescription
{

    [ExtensionOf(typeof(DesktopRootExtensionPoint),
    "HIS.Desktop.Plugins.InterconnectionPrescription",
    "liên thông đơn thuốc byt",
    "Common",
    69,
    "Lien_Thong_Don_Thuoc.png",
    "A",
    Module.MODULE_TYPE_ID__FORM,
    true,
    true)]
    public class InterconnectionPrescriptionProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public InterconnectionPrescriptionProcessor()
        {
            param = new CommonParam();
        }
        public InterconnectionPrescriptionProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IInterconnectionPrescription behavior = InterconnectionPrescriptionFactory.MakeIContactDrugstore(param, args);
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
