using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SetupMedicineTypeWithAcin.SetupMedicineTypeWithAcin
{
    class MedicineTypeWABehavior : Tool<IDesktopToolContext>, IMedicineTypeWA
    {
        object[] entity;
        long treatmentId;
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal MedicineTypeWABehavior()
            : base()
        {

        }

        internal MedicineTypeWABehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IMedicineTypeWA.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                //    foreach (var item in entity)
                //    {
                //        if (item is long)
                //        {
                //            treatmentId = (long)item;
                //        }
                //        else if (item is Inventec.Desktop.Common.Modules.Module)
                //        {
                //            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                //        }
                //        if (currentModule != null && treatmentId > 0)
                //        {
                //            result = new UCRoomAccount(currentModule, treatmentId);
                //            break;
                //        }
                //    }
                    result = new UC_SetupMedicineTypeWithAcin();
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
