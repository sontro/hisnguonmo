using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAppointmentServSO : StagingObjectBase
    {
        public HisAppointmentServSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_APPOINTMENT_SERV, bool>>> listHisAppointmentServExpression = new List<System.Linq.Expressions.Expression<Func<HIS_APPOINTMENT_SERV, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_APPOINTMENT_SERV, bool>>> listVHisAppointmentServExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_APPOINTMENT_SERV, bool>>>();
    }
}
