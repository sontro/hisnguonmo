using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentLogging
{
    partial class HisTreatmentLoggingGet : BusinessBase
    {
        internal V_HIS_TREATMENT_LOGGING GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisTreatmentLoggingViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_LOGGING GetViewByCode(string code, HisTreatmentLoggingViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentLoggingDAO.GetViewByCode(code, filter.Query());
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
