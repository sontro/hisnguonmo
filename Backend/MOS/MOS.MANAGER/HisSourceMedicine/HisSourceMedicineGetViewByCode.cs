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
        internal V_HIS_SOURCE_MEDICINE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisSourceMedicineViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SOURCE_MEDICINE GetViewByCode(string code, HisSourceMedicineViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSourceMedicineDAO.GetViewByCode(code, filter.Query());
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
