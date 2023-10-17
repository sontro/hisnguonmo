using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationStt
{
    partial class HisVaccinationSttGet : BusinessBase
    {
        internal List<V_HIS_VACCINATION_STT> GetView(HisVaccinationSttViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationSttDAO.GetView(filter.Query(), param);
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
