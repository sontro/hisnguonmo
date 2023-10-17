using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpUserTemp
{
    partial class HisImpUserTempGet : BusinessBase
    {
        internal HIS_IMP_USER_TEMP GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisImpUserTempFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_IMP_USER_TEMP GetByCode(string code, HisImpUserTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpUserTempDAO.GetByCode(code, filter.Query());
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
