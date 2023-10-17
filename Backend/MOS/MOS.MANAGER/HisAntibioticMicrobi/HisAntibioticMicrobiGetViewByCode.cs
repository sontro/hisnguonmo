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
        internal V_HIS_ANTIBIOTIC_MICROBI GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisAntibioticMicrobiViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ANTIBIOTIC_MICROBI GetViewByCode(string code, HisAntibioticMicrobiViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticMicrobiDAO.GetViewByCode(code, filter.Query());
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
