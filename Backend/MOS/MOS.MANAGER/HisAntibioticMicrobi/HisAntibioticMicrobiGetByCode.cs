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
        internal HIS_ANTIBIOTIC_MICROBI GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisAntibioticMicrobiFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIBIOTIC_MICROBI GetByCode(string code, HisAntibioticMicrobiFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticMicrobiDAO.GetByCode(code, filter.Query());
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
