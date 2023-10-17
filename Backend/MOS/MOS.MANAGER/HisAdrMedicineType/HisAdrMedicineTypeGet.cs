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
        internal HisAdrMedicineTypeGet()
            : base()
        {

        }

        internal HisAdrMedicineTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ADR_MEDICINE_TYPE> Get(HisAdrMedicineTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAdrMedicineTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ADR_MEDICINE_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisAdrMedicineTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ADR_MEDICINE_TYPE GetById(long id, HisAdrMedicineTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAdrMedicineTypeDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_ADR_MEDICINE_TYPE> GetByAdrId(long adrId)
        {
            try
            {
                HisAdrMedicineTypeFilterQuery filter = new HisAdrMedicineTypeFilterQuery();
                filter.ADR_ID = adrId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_ADR_MEDICINE_TYPE> GetByMedicineTypeId(long medicineTypeId)
        {
            try
            {
                HisAdrMedicineTypeFilterQuery filter = new HisAdrMedicineTypeFilterQuery();
                filter.MEDICINE_TYPE_ID = medicineTypeId;
                return this.Get(filter);
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
