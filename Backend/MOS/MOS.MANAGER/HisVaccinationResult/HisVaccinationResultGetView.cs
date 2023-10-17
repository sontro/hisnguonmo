using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationResult
{
    partial class HisVaccinationResultGet : BusinessBase
    {
        internal List<V_HIS_VACCINATION_RESULT> GetView(HisVaccinationResultViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationResultDAO.GetView(filter.Query(), param);
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
