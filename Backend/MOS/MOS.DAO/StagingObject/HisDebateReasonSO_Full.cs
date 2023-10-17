using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisDebateReasonSO : StagingObjectBase
    {
        public HisDebateReasonSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_DEBATE_REASON, bool>>> listHisDebateReasonExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DEBATE_REASON, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_DEBATE_REASON, bool>>> listVHisDebateReasonExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DEBATE_REASON, bool>>>();
    }
}
