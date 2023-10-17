using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportBlood.ExportBlood
{
    class ExportBloodBehavior : Tool<IDesktopToolContext>, IExportBlood
    {
        long expMestId = 0;
        V_HIS_EXP_MEST expMest = null;
        Inventec.Desktop.Common.Modules.Module moduleData;

        internal ExportBloodBehavior()
            : base()
        {

        }

        internal ExportBloodBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, long data)
            : base()
        {
            expMestId = data;
            moduleData = module;
        }

        internal ExportBloodBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, V_HIS_EXP_MEST data)
            : base()
        {
            expMest = data;
            moduleData = module;
        }

        object IExportBlood.Run()
        {
            object result = null;
            try
            {
                if (expMest != null)
                {
                    result = new frmExpMestBlood(moduleData, expMest);
                }
                else if (expMestId > 0)
                {
                    result = new frmExpMestBlood(moduleData, expMestId);
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
