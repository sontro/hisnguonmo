using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHospitalizeReason
{
    partial class HisHospitalizeReasonGet : BusinessBase
    {
        internal V_HIS_HOSPITALIZE_REASON GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisHospitalizeReasonViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_HOSPITALIZE_REASON GetViewByCode(string code, HisHospitalizeReasonViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHospitalizeReasonDAO.GetViewByCode(code, filter.Query());
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
