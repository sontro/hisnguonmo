using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationReact
{
    partial class HisVaccinationReactGet : BusinessBase
    {
        internal List<V_HIS_VACCINATION_REACT> GetView(HisVaccinationReactViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationReactDAO.GetView(filter.Query(), param);
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
