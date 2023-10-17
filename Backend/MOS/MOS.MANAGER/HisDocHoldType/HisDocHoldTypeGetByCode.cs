using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDocHoldType
{
    partial class HisDocHoldTypeGet : BusinessBase
    {
        internal HIS_DOC_HOLD_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisDocHoldTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DOC_HOLD_TYPE GetByCode(string code, HisDocHoldTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDocHoldTypeDAO.GetByCode(code, filter.Query());
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
