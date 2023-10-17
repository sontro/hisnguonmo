using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisEkipPlanUserSO : StagingObjectBase
    {
        public HisEkipPlanUserSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_EKIP_PLAN_USER, bool>>> listHisEkipPlanUserExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EKIP_PLAN_USER, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_EKIP_PLAN_USER, bool>>> listVHisEkipPlanUserExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EKIP_PLAN_USER, bool>>>();
    }
}
