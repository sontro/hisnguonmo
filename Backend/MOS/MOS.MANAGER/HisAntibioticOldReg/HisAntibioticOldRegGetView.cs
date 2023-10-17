using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticOldReg
{
    partial class HisAntibioticOldRegGet : BusinessBase
    {
        internal List<V_HIS_ANTIBIOTIC_OLD_REG> GetView(HisAntibioticOldRegViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticOldRegDAO.GetView(filter.Query(), param);
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
