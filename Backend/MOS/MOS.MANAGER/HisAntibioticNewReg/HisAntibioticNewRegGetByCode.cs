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
        internal HIS_ANTIBIOTIC_NEW_REG GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisAntibioticNewRegFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIBIOTIC_NEW_REG GetByCode(string code, HisAntibioticNewRegFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticNewRegDAO.GetByCode(code, filter.Query());
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
