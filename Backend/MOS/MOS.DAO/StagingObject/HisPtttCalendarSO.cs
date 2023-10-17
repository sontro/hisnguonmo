using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisPtttCalendarSO : StagingObjectBase
    {
        public HisPtttCalendarSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_PTTT_CALENDAR, bool>>> listHisPtttCalendarExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PTTT_CALENDAR, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_PTTT_CALENDAR, bool>>> listVHisPtttCalendarExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PTTT_CALENDAR, bool>>>();
    }
}
