using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticNewReg
{
    partial class HisAntibioticNewRegGet : BusinessBase
    {
        internal List<V_HIS_ANTIBIOTIC_NEW_REG> GetView(HisAntibioticNewRegViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticNewRegDAO.GetView(filter.Query(), param);
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
