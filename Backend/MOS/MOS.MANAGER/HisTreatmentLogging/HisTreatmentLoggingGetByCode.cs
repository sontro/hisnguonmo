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
        internal HIS_TREATMENT_LOGGING GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisTreatmentLoggingFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TREATMENT_LOGGING GetByCode(string code, HisTreatmentLoggingFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentLoggingDAO.GetByCode(code, filter.Query());
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
