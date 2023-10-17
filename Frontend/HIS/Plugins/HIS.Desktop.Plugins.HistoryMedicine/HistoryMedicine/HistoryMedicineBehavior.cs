using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.HistoryMedicine.HistoryMedicine;

namespace HIS.Desktop.Plugins.HistoryMedicine
{
    class HistoryMedicineBehavior : BusinessBase, IHistoryMedicine
    {
        object[] entity;
        long medicineTypeId;
        Inventec.Desktop.Common.Modules.Module module = null;
        internal HistoryMedicineBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHistoryMedicine.Run()
        {
            object rs = null;
            try
            {
                List<long> mediStockIds = null;
                string PACKAGE_NUMBER = "";

                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is long)
                            {
                                medicineTypeId = (long)entity[i];
                            }
                            else if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                module = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                            else if (entity[i] is string)
                            {
                                PACKAGE_NUMBER = (string)entity[i];
                            }
                        }
                    }
                }
                if (medicineTypeId > 0 && module != null && !string.IsNullOrEmpty(PACKAGE_NUMBER))
                {
                    rs = new frmHistory(module, medicineTypeId, PACKAGE_NUMBER);
                }
                else if (medicineTypeId > 0 && module != null)
                {
                    rs = new frmHistory(module, medicineTypeId);
                }

                else if (medicineTypeId == 0 && module != null)
                {
                    rs = new UC_HistoryMedicine(module);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
            return rs;
        }
    }
}
