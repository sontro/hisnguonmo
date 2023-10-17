using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttCalendar
{
    partial class HisPtttCalendarGet : BusinessBase
    {
        internal V_HIS_PTTT_CALENDAR GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisPtttCalendarViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_PTTT_CALENDAR GetViewByCode(string code, HisPtttCalendarViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPtttCalendarDAO.GetViewByCode(code, filter.Query());
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
