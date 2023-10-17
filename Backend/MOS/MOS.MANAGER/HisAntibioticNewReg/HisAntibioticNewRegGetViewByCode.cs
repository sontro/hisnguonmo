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
        internal V_HIS_ANTIBIOTIC_NEW_REG GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisAntibioticNewRegViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ANTIBIOTIC_NEW_REG GetViewByCode(string code, HisAntibioticNewRegViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticNewRegDAO.GetViewByCode(code, filter.Query());
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
