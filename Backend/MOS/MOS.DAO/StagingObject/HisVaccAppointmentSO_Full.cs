using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisVaccAppointmentSO : StagingObjectBase
    {
        public HisVaccAppointmentSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_VACC_APPOINTMENT, bool>>> listHisVaccAppointmentExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACC_APPOINTMENT, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_VACC_APPOINTMENT, bool>>> listVHisVaccAppointmentExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_VACC_APPOINTMENT, bool>>>();
    }
}
