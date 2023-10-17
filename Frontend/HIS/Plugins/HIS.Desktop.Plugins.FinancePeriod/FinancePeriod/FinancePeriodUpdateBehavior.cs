using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TYT.Desktop.Plugins.FinancePeriod;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace TYT.Desktop.Plugins.FinancePeriod.TYTFinancePeriod
{
    public sealed class TYTFinancePeriodUpdateBehavior : Tool<IDesktopToolContext>, ITYTFinancePeriod
    {
        Inventec.Desktop.Common.Modules.Module moduleData;
        long FinancePeriodId;
        DelegateSelectData refeshData;

        public TYTFinancePeriodUpdateBehavior()
            : base()
        {
        }

        public TYTFinancePeriodUpdateBehavior(CommonParam param,long FinancePeriodId, DelegateSelectData refeshData, Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {
            this.moduleData = moduleData;
            this.FinancePeriodId = FinancePeriodId;
            this.refeshData = refeshData;
        }

        object ITYTFinancePeriod.Run()
        {
            try
            {
                return new frmTYTFinancePeriod(this.moduleData, FinancePeriodId, refeshData);
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
