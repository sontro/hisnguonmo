using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisBranchTimeSO : StagingObjectBase
    {
        public HisBranchTimeSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_BRANCH_TIME, bool>>> listHisBranchTimeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BRANCH_TIME, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_BRANCH_TIME, bool>>> listVHisBranchTimeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BRANCH_TIME, bool>>>();
    }
}
