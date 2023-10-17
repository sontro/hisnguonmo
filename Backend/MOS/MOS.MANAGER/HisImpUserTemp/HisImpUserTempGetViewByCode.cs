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
        internal V_HIS_IMP_USER_TEMP GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisImpUserTempViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_IMP_USER_TEMP GetViewByCode(string code, HisImpUserTempViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpUserTempDAO.GetViewByCode(code, filter.Query());
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
