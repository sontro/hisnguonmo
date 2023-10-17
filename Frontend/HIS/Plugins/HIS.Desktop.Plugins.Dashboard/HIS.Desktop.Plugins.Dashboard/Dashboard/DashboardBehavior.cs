using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Dashboard.Dashboard
{
    class DashboardBehavior : BusinessBase, IDashboard
    {
        object entity;
        internal DashboardBehavior(CommonParam param, object filter)
            : base()
        {
            this.entity = filter;
        }

        object IDashboard.Run()
        {
            try
            {
                return new UCDashBoard();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
