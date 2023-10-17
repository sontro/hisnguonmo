using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisTestSampleTypeSO : StagingObjectBase
    {
        public HisTestSampleTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_TEST_SAMPLE_TYPE, bool>>> listHisTestSampleTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TEST_SAMPLE_TYPE, bool>>>();
    }
}
