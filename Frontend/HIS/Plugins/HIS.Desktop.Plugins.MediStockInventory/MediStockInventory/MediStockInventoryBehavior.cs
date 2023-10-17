using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockInventory.MediStockInventory
{
    class MediStockInventoryBehavior : Tool<IDesktopToolContext>, IMediStockInventory
    {
        Inventec.Desktop.Common.Modules.Module Module;
        internal MediStockInventoryBehavior()
            : base()
        {

        }

        internal MediStockInventoryBehavior(Inventec.Desktop.Common.Modules.Module moduleData, CommonParam param)
            : base()
        {
            Module = moduleData;
        }

        object IMediStockInventory.Run()
        {
            object result = null;
            try
            {
                result = new frmMediStockInventory(Module);
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
