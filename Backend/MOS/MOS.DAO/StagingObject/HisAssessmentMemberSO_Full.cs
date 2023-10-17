using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAssessmentMemberSO : StagingObjectBase
    {
        public HisAssessmentMemberSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_ASSESSMENT_MEMBER, bool>>> listHisAssessmentMemberExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ASSESSMENT_MEMBER, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_ASSESSMENT_MEMBER, bool>>> listVHisAssessmentMemberExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ASSESSMENT_MEMBER, bool>>>();
    }
}
