using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSourceMedicine
{
    partial class HisSourceMedicineGet : BusinessBase
    {
        internal HIS_SOURCE_MEDICINE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisSourceMedicineFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SOURCE_MEDICINE GetByCode(string code, HisSourceMedicineFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSourceMedicineDAO.GetByCode(code, filter.Query());
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
