using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAdrMedicineType
{
    partial class HisAdrMedicineTypeGet : BusinessBase
    {
        internal List<V_HIS_ADR_MEDICINE_TYPE> GetView(HisAdrMedicineTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAdrMedicineTypeDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_ADR_MEDICINE_TYPE> GetViewByAdrId(long adrId)
        {
            try
            {
                HisAdrMedicineTypeViewFilterQuery filter = new HisAdrMedicineTypeViewFilterQuery();
                filter.ADR_ID = adrId;
                return this.GetView(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_ADR_MEDICINE_TYPE> GetViewByMedicineTypeId(long medicineTypeId)
        {
            try
            {
                HisAdrMedicineTypeViewFilterQuery filter = new HisAdrMedicineTypeViewFilterQuery();
                filter.MEDICINE_TYPE_ID = medicineTypeId;
                return this.GetView(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
