using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServRation
{
    partial class HisSereServRationGet : BusinessBase
    {
        internal List<V_HIS_SERE_SERV_RATION> GetView(HisSereServRationViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServRationDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERE_SERV_RATION> GetViewByTreatmentId(long treatmentId)
        {
            try
            {
                HisSereServRationViewFilterQuery filter = new HisSereServRationViewFilterQuery();
                filter.TREATMENT_ID = treatmentId;
                return this.GetView(filter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return null;
        }
    }
}
