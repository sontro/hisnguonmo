using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateType
{
    partial class HisDebateTypeGet : BusinessBase
    {
        internal HIS_DEBATE_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisDebateTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEBATE_TYPE GetByCode(string code, HisDebateTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateTypeDAO.GetByCode(code, filter.Query());
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
