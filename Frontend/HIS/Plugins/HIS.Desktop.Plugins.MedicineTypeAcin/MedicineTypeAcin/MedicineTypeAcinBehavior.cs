using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineTypeAcin.MedicineTypeAcin
{
    class MedicineTypeAcinBehavior : Tool<IDesktopToolContext>, IMedicineTypeAcin
    {
        object[] entity;
        long treatmentId;
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal MedicineTypeAcinBehavior()
            : base()
        {

        }

        internal MedicineTypeAcinBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IMedicineTypeAcin.Run()
        {
            object result = null;
            long medicineTypeId = 0;
            Inventec.Desktop.Common.Modules.Module currentModule = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is long)
                        {
                            medicineTypeId = (long)item;
                        }
                        else if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                    }
                    result = new frmMedicineTypeAcin(medicineTypeId);
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
