using HIS.Desktop.Plugins.ReportCreate;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;

namespace Inventec.Desktop.Plugins.ReportCreate.ReportCreate
{
    public sealed class ReportCreateBehavior : Tool<IDesktopToolContext>, IReportCreate
    {
        object entity;
        public ReportCreateBehavior()
            : base()
        {
        }

        public ReportCreateBehavior(CommonParam param, object filter)
            : base()
        {
            this.entity = filter;
        }

        object IReportCreate.Run()
        {
            try
            {
                return new frmMainReport();
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
