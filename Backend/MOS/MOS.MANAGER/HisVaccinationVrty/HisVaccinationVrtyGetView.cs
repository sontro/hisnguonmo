using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationVrty
{
    partial class HisVaccinationVrtyGet : BusinessBase
    {
        internal List<V_HIS_VACCINATION_VRTY> GetView(HisVaccinationVrtyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationVrtyDAO.GetView(filter.Query(), param);
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
