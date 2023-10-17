using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisTestIndexGroupSO : StagingObjectBase
    {
        public HisTestIndexGroupSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_TEST_INDEX_GROUP, bool>>> listHisTestIndexGroupExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TEST_INDEX_GROUP, bool>>>();
    }
}
