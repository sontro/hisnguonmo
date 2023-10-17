using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPosition
{
    partial class HisPositionGet : BusinessBase
    {
        internal HIS_POSITION GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisPositionFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_POSITION GetByCode(string code, HisPositionFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPositionDAO.GetByCode(code, filter.Query());
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
