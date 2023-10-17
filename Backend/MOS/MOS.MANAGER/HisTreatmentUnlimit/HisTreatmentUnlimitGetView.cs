using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentUnlimit
{
    partial class HisTreatmentUnlimitGet : BusinessBase
    {
        internal List<V_HIS_TREATMENT_UNLIMIT> GetView(HisTreatmentUnlimitViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentUnlimitDAO.GetView(filter.Query(), param);
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
