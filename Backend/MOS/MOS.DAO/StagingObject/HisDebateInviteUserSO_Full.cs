using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisDebateInviteUserSO : StagingObjectBase
    {
        public HisDebateInviteUserSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_DEBATE_INVITE_USER, bool>>> listHisDebateInviteUserExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DEBATE_INVITE_USER, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_DEBATE_INVITE_USER, bool>>> listVHisDebateInviteUserExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DEBATE_INVITE_USER, bool>>>();
    }
}
