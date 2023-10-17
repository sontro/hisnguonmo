using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisExamScheduleSO : StagingObjectBase
    {
        public HisExamScheduleSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_EXAM_SCHEDULE, bool>>> listHisExamScheduleExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EXAM_SCHEDULE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_EXAM_SCHEDULE, bool>>> listVHisExamScheduleExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EXAM_SCHEDULE, bool>>>();
    }
}
