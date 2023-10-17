using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAppointmentPeriodSO : StagingObjectBase
    {
        public HisAppointmentPeriodSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_APPOINTMENT_PERIOD, bool>>> listHisAppointmentPeriodExpression = new List<System.Linq.Expressions.Expression<Func<HIS_APPOINTMENT_PERIOD, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_APPOINTMENT_PERIOD, bool>>> listVHisAppointmentPeriodExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_APPOINTMENT_PERIOD, bool>>>();
    }
}
