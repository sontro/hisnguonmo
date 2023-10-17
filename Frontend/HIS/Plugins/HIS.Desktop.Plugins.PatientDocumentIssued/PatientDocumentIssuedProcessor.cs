using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PatientDocumentIssued
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.PatientDocumentIssued",
           "Cấp văn bản cho bệnh nhân",
           "Common",
           3,
           "tu-benh-an.png",
           "A",
           Module.MODULE_TYPE_ID__UC,
           true,
           true)
        ]

    class PatientDocumentIssuedProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public PatientDocumentIssuedProcessor()
        {
            param = new CommonParam();
        }
        public PatientDocumentIssuedProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IPatientDocumentIssued behavior = PatientDocumentIssuedFactory.MakeIControl(param, args);
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
