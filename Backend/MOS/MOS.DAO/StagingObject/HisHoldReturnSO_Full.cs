using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisHoldReturnSO : StagingObjectBase
    {
        public HisHoldReturnSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_HOLD_RETURN, bool>>> listHisHoldReturnExpression = new List<System.Linq.Expressions.Expression<Func<HIS_HOLD_RETURN, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_HOLD_RETURN, bool>>> listVHisHoldReturnExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_HOLD_RETURN, bool>>>();
    }
}
