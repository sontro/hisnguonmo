using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBidMedicineType
{
    partial class HisBidMedicineTypeGet : BusinessBase
    {
        internal HIS_BID_MEDICINE_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisBidMedicineTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BID_MEDICINE_TYPE GetByCode(string code, HisBidMedicineTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBidMedicineTypeDAO.GetByCode(code, filter.Query());
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
