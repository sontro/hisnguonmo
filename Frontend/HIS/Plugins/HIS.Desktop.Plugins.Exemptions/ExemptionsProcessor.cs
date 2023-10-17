using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Exemptions
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.Exemptions",
        "Miễn giảm",
        "Common",
        59,
        "",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class ExemptionsProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExemptionsProcessor()
        {
            param = new CommonParam();
        }
        public ExemptionsProcessor(CommonParam paramBusiness)
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
                HIS_SERE_SERV _sereServ = null;

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
                            else if (args[i] is HIS_SERE_SERV)
                            {
                                _sereServ = (HIS_SERE_SERV)args[i];
                            }
                        }
                    }
                }

                if (moduleData != null)
                {
                    if (_sereServ != null && _sereServ.ID > 0)
                    {
                        result = new frmExemptions(moduleData, _sereServ);
                    }
                    else if (_treatmentId > 0)
                    {
                        result = new frmExemptions(moduleData, _treatmentId);
                    }
                    else
                        result = new frmExemptions(moduleData);
                }
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
            return false;
        }
    }
}
