using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisRationScheduleSO : StagingObjectBase
    {
        public HisRationScheduleSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_RATION_SCHEDULE, bool>>> listHisRationScheduleExpression = new List<System.Linq.Expressions.Expression<Func<HIS_RATION_SCHEDULE, bool>>>();
    }
}
