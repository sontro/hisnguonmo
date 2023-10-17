using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisDebateTypeSO : StagingObjectBase
    {
        public HisDebateTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_DEBATE_TYPE, bool>>> listHisDebateTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DEBATE_TYPE, bool>>>();
    }
}
