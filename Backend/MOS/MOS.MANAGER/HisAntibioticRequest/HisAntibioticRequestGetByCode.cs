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
        internal HIS_ANTIBIOTIC_REQUEST GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisAntibioticRequestFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIBIOTIC_REQUEST GetByCode(string code, HisAntibioticRequestFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticRequestDAO.GetByCode(code, filter.Query());
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
