using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticMicrobi
{
    partial class HisAntibioticMicrobiGet : BusinessBase
    {
        internal List<V_HIS_ANTIBIOTIC_MICROBI> GetView(HisAntibioticMicrobiViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticMicrobiDAO.GetView(filter.Query(), param);
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
