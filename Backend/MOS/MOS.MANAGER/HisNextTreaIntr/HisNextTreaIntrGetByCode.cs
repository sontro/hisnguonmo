using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNextTreaIntr
{
    partial class HisNextTreaIntrGet : BusinessBase
    {
        internal HIS_NEXT_TREA_INTR GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisNextTreaIntrFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_NEXT_TREA_INTR GetByCode(string code, HisNextTreaIntrFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisNextTreaIntrDAO.GetByCode(code, filter.Query());
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
