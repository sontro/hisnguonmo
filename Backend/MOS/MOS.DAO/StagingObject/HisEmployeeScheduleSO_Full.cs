using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisEmployeeScheduleSO : StagingObjectBase
    {
        public HisEmployeeScheduleSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_EMPLOYEE_SCHEDULE, bool>>> listHisEmployeeScheduleExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EMPLOYEE_SCHEDULE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_EMPLOYEE_SCHEDULE, bool>>> listVHisEmployeeScheduleExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EMPLOYEE_SCHEDULE, bool>>>();
    }
}
