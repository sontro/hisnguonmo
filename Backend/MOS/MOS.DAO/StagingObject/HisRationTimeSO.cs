using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisRationTimeSO : StagingObjectBase
    {
        public HisRationTimeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_RATION_TIME, bool>>> listHisRationTimeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_RATION_TIME, bool>>>();
    }
}
