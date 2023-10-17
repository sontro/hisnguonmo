using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.FinancePeriod;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.FinancePeriod.FinancePeriod
{
    public sealed class TYTFinancePeriodBehavior : Tool<IDesktopToolContext>, IFinancePeriod
    {
        Inventec.Desktop.Common.Modules.Module moduleData;
        public TYTFinancePeriodBehavior()
            : base()
        {
        }

        public TYTFinancePeriodBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {
            this.moduleData = moduleData;
        }

        object IFinancePeriod.Run()
        {
            try
            {
                return new UCFinancePeriod(this.moduleData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                //param.HasException = true;
                return null;
            }
        }
    }
}
