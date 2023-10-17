using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRefectory
{
    partial class HisRefectoryGet : BusinessBase
    {
        internal V_HIS_REFECTORY GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisRefectoryViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_REFECTORY GetViewByCode(string code, HisRefectoryViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRefectoryDAO.GetViewByCode(code, filter.Query());
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
