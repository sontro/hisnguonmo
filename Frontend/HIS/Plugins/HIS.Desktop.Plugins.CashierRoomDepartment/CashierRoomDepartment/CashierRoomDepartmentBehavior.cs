using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.CashierRoomDepartment.CashierRoomDepartment
{
    class CashierRoomDepartmentBehavior : Tool<IDesktopToolContext>, ICashierRoomDepartment
    {
        object[] entity;
        long treatmentId;
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal CashierRoomDepartmentBehavior()
            : base()
        {

        }

        internal CashierRoomDepartmentBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object ICashierRoomDepartment.Run()
        {
            object result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
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
                    }
                    result = new UCCashierRoomDepartment(currentModule);
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
