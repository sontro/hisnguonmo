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
        internal HIS_IMP_USER_TEMP_DT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisImpUserTempDtFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_IMP_USER_TEMP_DT GetByCode(string code, HisImpUserTempDtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpUserTempDtDAO.GetByCode(code, filter.Query());
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
