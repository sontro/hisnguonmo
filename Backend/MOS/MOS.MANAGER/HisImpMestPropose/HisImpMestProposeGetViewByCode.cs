using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestPropose
{
    partial class HisImpMestProposeGet : BusinessBase
    {
        internal V_HIS_IMP_MEST_PROPOSE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisImpMestProposeViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_IMP_MEST_PROPOSE GetViewByCode(string code, HisImpMestProposeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestProposeDAO.GetViewByCode(code, filter.Query());
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
