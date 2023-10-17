using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientCase
{
    partial class HisPatientCaseGet : BusinessBase
    {
        internal List<V_HIS_PATIENT_CASE> GetView(HisPatientCaseViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPatientCaseDAO.GetView(filter.Query(), param);
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
