using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMedicineTypeAcin.HisMedicineTypeAcin
{
    class HisMedicineTypeAcinBehavior : Tool<IDesktopToolContext>, IHisMedicineTypeAcin
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        DelegateReturnMutilObject resultActiveIngredient;
        List<V_HIS_MEDICINE_TYPE_ACIN> listMedicineTypeAcin;
        internal HisMedicineTypeAcinBehavior()
            : base()
        {

        }

        internal HisMedicineTypeAcinBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IHisMedicineTypeAcin.Run()
        {
            object result = null;
            long medicineTypeId = 0;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is long)
                        {
                            medicineTypeId = (long)entity[i];
                        }
                        else if (entity[i] is DelegateReturnMutilObject)
                        {
                            resultActiveIngredient = (DelegateReturnMutilObject)entity[i];
                        }

                        else if (entity[i] is List<V_HIS_MEDICINE_TYPE_ACIN>)
                        {
                            listMedicineTypeAcin = (List<V_HIS_MEDICINE_TYPE_ACIN>)entity[i];
                        }
                    }
                    if (resultActiveIngredient != null && listMedicineTypeAcin!=null)
                    {
                        result = new frmHisMedicineTypeAcin(medicineTypeId, resultActiveIngredient, listMedicineTypeAcin);
                    }
                    else
                    {
                        result = new frmHisMedicineTypeAcin(medicineTypeId);
                    }
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
