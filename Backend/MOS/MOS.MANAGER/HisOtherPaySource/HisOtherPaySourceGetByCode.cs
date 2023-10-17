using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisOtherPaySource
{
    partial class HisOtherPaySourceGet : BusinessBase
    {
        internal HIS_OTHER_PAY_SOURCE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisOtherPaySourceFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_OTHER_PAY_SOURCE GetByCode(string code, HisOtherPaySourceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisOtherPaySourceDAO.GetByCode(code, filter.Query());
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
