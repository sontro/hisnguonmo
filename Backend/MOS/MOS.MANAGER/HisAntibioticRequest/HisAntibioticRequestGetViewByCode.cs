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
        internal V_HIS_ANTIBIOTIC_REQUEST GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisAntibioticRequestViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ANTIBIOTIC_REQUEST GetViewByCode(string code, HisAntibioticRequestViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticRequestDAO.GetViewByCode(code, filter.Query());
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
