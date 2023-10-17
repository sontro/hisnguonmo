using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDesk
{
    partial class HisDeskGet : BusinessBase
    {
        internal V_HIS_DESK GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisDeskViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DESK GetViewByCode(string code, HisDeskViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDeskDAO.GetViewByCode(code, filter.Query());
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
