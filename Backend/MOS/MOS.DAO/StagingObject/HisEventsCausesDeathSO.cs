using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisEventsCausesDeathSO : StagingObjectBase
    {
        public HisEventsCausesDeathSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_EVENTS_CAUSES_DEATH, bool>>> listHisEventsCausesDeathExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EVENTS_CAUSES_DEATH, bool>>>();
    }
}
