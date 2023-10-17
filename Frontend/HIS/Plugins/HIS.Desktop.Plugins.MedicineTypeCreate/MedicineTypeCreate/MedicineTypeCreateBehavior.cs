using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Linq;

namespace HIS.Desktop.Plugins.MedicineTypeCreate.MedicineTypeCreate
{
    class MedicineTypeCreateBehavior : BusinessBase, IMedicineTypeCreate
    {
        object[] entity;
        internal MedicineTypeCreateBehavior(CommonParam param, object[] data)
            : base()
        {
            try
            {
                entity = data;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        object IMedicineTypeCreate.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long? currentVHisMedicineTypeIdDefault = null;
                int actionType = 0;
                DelegateSelectData delegateSelect = null;
                DelegateRefreshData delegateRefreshData = null;

                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                           
                            if (entity[i] is long)
                            {
                                currentVHisMedicineTypeIdDefault = (long?)entity[i];
                            }
                            if (entity[i] is int)
                            {
                                actionType = (int)entity[i];
                            }
                            if (entity[i] is DelegateSelectData)
                            {
                                delegateSelect = (DelegateSelectData)entity[i];
                            }
                            if (entity[i] is DelegateRefreshData)
                            {
                                delegateRefreshData = (DelegateRefreshData)entity[i];
                            }
                        }
                    }
                }

                return new frmMedicineTypeCreate(moduleData, currentVHisMedicineTypeIdDefault, actionType, delegateSelect);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
