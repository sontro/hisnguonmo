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
        internal V_HIS_MIXED_MEDICINE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMixedMedicineViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MIXED_MEDICINE GetViewByCode(string code, HisMixedMedicineViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMixedMedicineDAO.GetViewByCode(code, filter.Query());
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
