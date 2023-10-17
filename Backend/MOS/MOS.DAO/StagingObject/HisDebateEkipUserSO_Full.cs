using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisDebateEkipUserSO : StagingObjectBase
    {
        public HisDebateEkipUserSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_DEBATE_EKIP_USER, bool>>> listHisDebateEkipUserExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DEBATE_EKIP_USER, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_DEBATE_EKIP_USER, bool>>> listVHisDebateEkipUserExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DEBATE_EKIP_USER, bool>>>();
    }
}
