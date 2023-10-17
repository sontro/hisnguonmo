using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpUserTempDt
{
    partial class HisImpUserTempDtGet : BusinessBase
    {
        internal V_HIS_IMP_USER_TEMP_DT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisImpUserTempDtViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_IMP_USER_TEMP_DT GetViewByCode(string code, HisImpUserTempDtViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpUserTempDtDAO.GetViewByCode(code, filter.Query());
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
