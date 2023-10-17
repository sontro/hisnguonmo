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
        internal V_HIS_POSITION GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisPositionViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_POSITION GetViewByCode(string code, HisPositionViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPositionDAO.GetViewByCode(code, filter.Query());
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
