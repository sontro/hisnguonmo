using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisHealthExamRankSO : StagingObjectBase
    {
        public HisHealthExamRankSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_HEALTH_EXAM_RANK, bool>>> listHisHealthExamRankExpression = new List<System.Linq.Expressions.Expression<Func<HIS_HEALTH_EXAM_RANK, bool>>>();
    }
}
