using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisEkipPlanSO : StagingObjectBase
    {
        public HisEkipPlanSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_EKIP_PLAN, bool>>> listHisEkipPlanExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EKIP_PLAN, bool>>>();
    }
}
