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
        internal List<V_HIS_HOSPITALIZE_REASON> GetView(HisHospitalizeReasonViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHospitalizeReasonDAO.GetView(filter.Query(), param);
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
