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
        internal List<V_HIS_TREATMENT_LOGGING> GetView(HisTreatmentLoggingViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentLoggingDAO.GetView(filter.Query(), param);
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
