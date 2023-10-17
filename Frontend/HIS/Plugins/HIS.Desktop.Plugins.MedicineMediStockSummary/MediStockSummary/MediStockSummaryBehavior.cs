using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.MedicineMediStockSummary;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.MedicineMediStockSummary.MediStockSummary
{
    public sealed class MediStockSummaryBehavior : Tool<IDesktopToolContext>, IMediStockSummary
    {
        object[] entity;
        public MediStockSummaryBehavior()
            : base()
        {
        }

        public MediStockSummaryBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IMediStockSummary.Run()
        {
            object result = null;
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
                if (moduleData != null)
                {
                    return new ucMediaStockSummaryByMedicine(moduleData.RoomId, moduleData.RoomTypeId);
                  
                }
                else
                {
                    return null;
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
