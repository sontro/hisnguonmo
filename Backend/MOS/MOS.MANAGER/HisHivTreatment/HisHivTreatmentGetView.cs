using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHivTreatment
{
    partial class HisHivTreatmentGet : BusinessBase
    {
        internal List<V_HIS_HIV_TREATMENT> GetView(HisHivTreatmentViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHivTreatmentDAO.GetView(filter.Query(), param);
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
