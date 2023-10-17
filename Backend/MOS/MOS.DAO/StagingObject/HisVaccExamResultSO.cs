using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisVaccExamResultSO : StagingObjectBase
    {
        public HisVaccExamResultSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_VACC_EXAM_RESULT, bool>>> listHisVaccExamResultExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACC_EXAM_RESULT, bool>>>();
    }
}
