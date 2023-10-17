using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticRequest
{
    partial class HisAntibioticRequestGet : BusinessBase
    {
        internal List<V_HIS_ANTIBIOTIC_REQUEST> GetView(HisAntibioticRequestViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticRequestDAO.GetView(filter.Query(), param);
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
