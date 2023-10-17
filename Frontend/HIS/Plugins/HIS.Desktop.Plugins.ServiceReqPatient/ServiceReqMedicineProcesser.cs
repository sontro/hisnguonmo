using Inventec.Core;
using Inventec.Desktop.Common;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.Common;
using HIS.Desktop.Plugins.ServiceReqPatient;


namespace HIS.Desktop.Plugins.ServiceReqPatient
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
     "HIS.Desktop.Plugins.ServiceReqPatient",
     "Danh mục",
     "Bussiness",
     4,
     "showproduct_32x32.png",
     "A",
     Module.MODULE_TYPE_ID__FORM,
     true,
     true)
  ]
    public class ServiceReqPatientProcessor : ModuleBase, IDesktopRoot
    {

        CommonParam param;
        public ServiceReqPatientProcessor()
        {
            param = new CommonParam();
        }
        public ServiceReqPatientProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long? _PatientId = null;
                String DeparmentName = "";
                if (args.GetType() == typeof(object[]))
                {
                    if (args != null && args.Count() > 0)
                    {
                        for (int i = 0; i < args.Count(); i++)
                        {
                            if (args[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)args[i];
                            }
                            else if (args[i] is long?)
                            {
                                _PatientId = (long?)args[i];
                            }
                            else if (args[i] is string)
                            {
                                DeparmentName = (string)args[i];
                            }

                        }
                    }
                }

                if (_PatientId == null)
                    result = new HIS.Desktop.Plugins.ServiceReqPatient.ServiceReqPatientForm(moduleData);
                else
                    result = new HIS.Desktop.Plugins.ServiceReqPatient.ServiceReqPatientForm(moduleData, _PatientId, DeparmentName);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
