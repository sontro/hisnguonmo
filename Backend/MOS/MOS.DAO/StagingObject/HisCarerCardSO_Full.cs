using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisCarerCardSO : StagingObjectBase
    {
        public HisCarerCardSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_CARER_CARD, bool>>> listHisCarerCardExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CARER_CARD, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_CARER_CARD, bool>>> listVHisCarerCardExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_CARER_CARD, bool>>>();
    }
}
