using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RevenueList.RevenueList
{
    class RevenueListBehavior : Tool<IDesktopToolContext>, IRevenueList
    {
        object[] entity;

        internal RevenueListBehavior()
            : base()
        {

        }

        internal RevenueListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IRevenueList.Run()
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
                return new UCRevenueList(moduleData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
