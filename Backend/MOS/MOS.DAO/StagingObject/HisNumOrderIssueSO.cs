using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisNumOrderIssueSO : StagingObjectBase
    {
        public HisNumOrderIssueSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_NUM_ORDER_ISSUE, bool>>> listHisNumOrderIssueExpression = new List<System.Linq.Expressions.Expression<Func<HIS_NUM_ORDER_ISSUE, bool>>>();
    }
}
