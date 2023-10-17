using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PeriodDepartmentList.PeriodDepartmentList
{
    class PeriodDepartmentListBehavior : Tool<DesktopToolContext>, IPeriodDepartmentList
    {
        object[] entity;

        internal PeriodDepartmentListBehavior()
            : base()
        {

        }

        internal PeriodDepartmentListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IPeriodDepartmentList.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                    }
                }
                return new UCPeriodDepartmentList(moduleData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
