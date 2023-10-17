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
        internal HIS_HOSPITALIZE_REASON GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisHospitalizeReasonFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HOSPITALIZE_REASON GetByCode(string code, HisHospitalizeReasonFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHospitalizeReasonDAO.GetByCode(code, filter.Query());
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
