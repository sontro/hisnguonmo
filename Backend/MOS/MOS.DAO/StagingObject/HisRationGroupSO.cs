using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisRationGroupSO : StagingObjectBase
    {
        public HisRationGroupSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_RATION_GROUP, bool>>> listHisRationGroupExpression = new List<System.Linq.Expressions.Expression<Func<HIS_RATION_GROUP, bool>>>();
    }
}
