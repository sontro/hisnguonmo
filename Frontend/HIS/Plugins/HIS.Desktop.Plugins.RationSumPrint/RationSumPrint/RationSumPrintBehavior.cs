using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.RationSumPrint.RationSumPrint
{
    class RationSumPrintBehavior : Tool<IDesktopToolContext>, IRationSumPrint
    {
        object[] entity;

        internal RationSumPrintBehavior()
            : base()
        {

        }

        internal RationSumPrintBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IRationSumPrint.Run()
        {
            List<MOS.EFMODEL.DataModels.V_HIS_RATION_SUM> result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is List<MOS.EFMODEL.DataModels.V_HIS_RATION_SUM>)
                            result = (List<MOS.EFMODEL.DataModels.V_HIS_RATION_SUM>)item;
                        if (item is Inventec.Desktop.Common.Modules.Module) 
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                }
                if (moduleData != null)
                {
                    return new frmRationSumPrint(result, moduleData);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
