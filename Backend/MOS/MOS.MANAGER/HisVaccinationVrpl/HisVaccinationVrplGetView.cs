using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationVrpl
{
    partial class HisVaccinationVrplGet : BusinessBase
    {
        internal List<V_HIS_VACCINATION_VRPL> GetView(HisVaccinationVrplViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationVrplDAO.GetView(filter.Query(), param);
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
