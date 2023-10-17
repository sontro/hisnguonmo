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
        internal List<V_HIS_PTTT_CALENDAR> GetView(HisPtttCalendarViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPtttCalendarDAO.GetView(filter.Query(), param);
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
