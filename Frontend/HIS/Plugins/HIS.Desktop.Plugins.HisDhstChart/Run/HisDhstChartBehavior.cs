using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.HisDhstChart;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;
using HIS.Desktop.Plugins.HisDhstChart.Run;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.HisDhstChart.Run
{
    public sealed class HisDhstChartBehavior : Tool<IDesktopToolContext>, IHisDhstChart
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        long treatmentId = 0;
        List<HIS_DHST> currentDhst;
        public HisDhstChartBehavior()
            : base()
        {
        }

        public HisDhstChartBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisDhstChart.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is long)
                        {
                            treatmentId = (long)item;
                        }
                        else if (item is List<HIS_DHST>)
                        {
                            currentDhst = (List<HIS_DHST>)item;
                        }
                    }
                    if (currentModule != null && currentDhst != null)
                    {
                        result = new frmHisDhstChart(currentModule, currentDhst);
                    }
                    else if (currentModule != null && treatmentId > 0)
                    {
                        result = new frmHisDhstChart(currentModule, treatmentId);
                    }
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
