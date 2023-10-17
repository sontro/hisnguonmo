using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisTreatmentFile
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
     "HIS.Desktop.Plugins.HisTreatmentFile",
     "Danh mục",
     "Bussiness",
     4,
     "showproduct_32x32.png",
     "A",
     Module.MODULE_TYPE_ID__FORM,
     true,
     true)
  ]
    class HisTreatmentFileProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisTreatmentFileProcessor()
        {
            param = new CommonParam();
        }
        public HisTreatmentFileProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long _treatmentId = 0;

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
                            else if (args[i] is long)
                            {
                                _treatmentId = (long)args[i];
                            }
                        }
                    }
                }

                if (_treatmentId > 0)
                {
                    Inventec.Common.Logging.LogSystem.Debug("Data________________________________________" + moduleData);
                    result = new HIS.Desktop.Plugins.HisTreatmentFile.frmTreatmentFile(moduleData, _treatmentId);
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug("1________________________________________");

                    result = new HIS.Desktop.Plugins.HisTreatmentFile.frmTreatmentFile(moduleData);
                }
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
