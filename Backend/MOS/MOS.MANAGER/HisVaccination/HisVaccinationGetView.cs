using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccination
{
    partial class HisVaccinationGet : BusinessBase
    {
        internal List<V_HIS_VACCINATION> GetView(HisVaccinationViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_VACCINATION> GetViewByIds(List<long> ids)
        {
            if (ids != null)
            {
                HisVaccinationViewFilterQuery filter = new HisVaccinationViewFilterQuery();
                filter.IDs = ids;
                return this.GetView(filter);
            }
            return null;
        }
    }
}
