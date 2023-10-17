using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisTestTypeSO : StagingObjectBase
    {
        public HisTestTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_TEST_TYPE, bool>>> listHisTestTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TEST_TYPE, bool>>>();
    }
}
