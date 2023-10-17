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
        internal V_HIS_ANTIBIOTIC_OLD_REG GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisAntibioticOldRegViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ANTIBIOTIC_OLD_REG GetViewByCode(string code, HisAntibioticOldRegViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticOldRegDAO.GetViewByCode(code, filter.Query());
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
