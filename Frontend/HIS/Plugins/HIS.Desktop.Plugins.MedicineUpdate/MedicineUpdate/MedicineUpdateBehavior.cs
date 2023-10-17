using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.MedicineUpdate.MedicineUpdate
{
    class MedicineUpdateBehavior : Tool<IDesktopToolContext>, IMedicineUpdate
    {
        object[] entity;

        internal MedicineUpdateBehavior()
            : base()
        {

        }

        internal MedicineUpdateBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IMedicineUpdate.Run()
        {
            MOS.EFMODEL.DataModels.V_HIS_MEDICINE_1 result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is MOS.EFMODEL.DataModels.V_HIS_MEDICINE_1)
                            result = (MOS.EFMODEL.DataModels.V_HIS_MEDICINE_1)item;
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                }
                if (moduleData != null)
                {
                    return new FormMedicineUpdate(result, moduleData);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
