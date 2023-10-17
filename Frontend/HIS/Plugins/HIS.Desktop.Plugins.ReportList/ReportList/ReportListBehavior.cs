using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ReportList;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace Inventec.Desktop.Plugins.ReportList.ReportList
{
    public sealed class ReportListBehavior : Tool<IDesktopToolContext>, IReportList
    {
        object entity;
        public ReportListBehavior()
            : base()
        {
        }

        public ReportListBehavior(CommonParam param, object filter)
            : base()
        {
            this.entity = filter;
        }

        object IReportList.Run()
        {
            try
            {
                return new UCReportList();
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
