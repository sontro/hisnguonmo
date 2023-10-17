using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ReportTypeList;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using Inventec.Common.Logging;

namespace Inventec.Desktop.Plugins.ReportTypeList.ReportTypeList
{
    public sealed class ReportTypeListBehavior : Tool<IDesktopToolContext>, IReportTypeList
    {
        object[] entity;
        public ReportTypeListBehavior()
            : base()
        {
        }

        public ReportTypeListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IReportTypeList.Run()
        {
            try
            {
                Inventec.UC.ListReportType.CreateReport_Click createReportClick = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.UC.ListReportType.CreateReport_Click)
                        {
                            createReportClick = (Inventec.UC.ListReportType.CreateReport_Click)entity[i];
                        }
                    }
                }
               
                return new UCReportTypeList(createReportClick);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
