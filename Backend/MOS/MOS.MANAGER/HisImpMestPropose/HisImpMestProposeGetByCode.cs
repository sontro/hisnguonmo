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
        internal HIS_IMP_MEST_PROPOSE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisImpMestProposeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_IMP_MEST_PROPOSE GetByCode(string code, HisImpMestProposeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestProposeDAO.GetByCode(code, filter.Query());
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
