using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMixedMedicine
{
    partial class HisMixedMedicineGet : BusinessBase
    {
        internal HIS_MIXED_MEDICINE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMixedMedicineFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MIXED_MEDICINE GetByCode(string code, HisMixedMedicineFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMixedMedicineDAO.GetByCode(code, filter.Query());
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
