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
        internal HIS_ANTIBIOTIC_OLD_REG GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisAntibioticOldRegFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIBIOTIC_OLD_REG GetByCode(string code, HisAntibioticOldRegFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticOldRegDAO.GetByCode(code, filter.Query());
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
