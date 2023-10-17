using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedLog
{
    partial class HisBedLogGet : BusinessBase
    {
        internal List<V_HIS_BED_LOG> GetView(HisBedLogViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBedLogDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_BED_LOG> GetViewByTreatmentId(long treatmentId)
        {
            try
            {
                HisBedLogViewFilterQuery filter = new HisBedLogViewFilterQuery();
                filter.TREATMENT_ID = treatmentId;
                return this.GetView(filter);
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
