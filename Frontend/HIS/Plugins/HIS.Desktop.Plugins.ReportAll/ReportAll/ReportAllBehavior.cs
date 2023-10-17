using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ReportAll;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace Inventec.Desktop.Plugins.ReportAll.ReportAll
{
    public sealed class ReportAllBehavior : Tool<IDesktopToolContext>, IReportAll
    {
        object[] entity;
        public ReportAllBehavior()
            : base()
        {
        }

        public ReportAllBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IReportAll.Run()
        {
            try
            {
              Inventec.UC.ListReportType.CreateReport_Click createReportClick = null;
              Inventec.Desktop.Common.Modules.Module moduleData = null;
              if (entity != null && entity.Count() > 0)
              {
                for (int i = 0; i < entity.Count(); i++)
                {
                  if (entity[i] is Inventec.UC.ListReportType.CreateReport_Click)
                  {
                    createReportClick = (Inventec.UC.ListReportType.CreateReport_Click)entity[i];
                  }
                  if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                  {
                      moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                  }
                }
              }
              return new UCReportAll(createReportClick, moduleData);
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
