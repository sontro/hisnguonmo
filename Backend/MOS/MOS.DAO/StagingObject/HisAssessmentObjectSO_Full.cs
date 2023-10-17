using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAssessmentObjectSO : StagingObjectBase
    {
        public HisAssessmentObjectSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_ASSESSMENT_OBJECT, bool>>> listHisAssessmentObjectExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ASSESSMENT_OBJECT, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_ASSESSMENT_OBJECT, bool>>> listVHisAssessmentObjectExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ASSESSMENT_OBJECT, bool>>>();
    }
}
