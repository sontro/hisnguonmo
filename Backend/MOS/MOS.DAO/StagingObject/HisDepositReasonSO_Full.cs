using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisDepositReasonSO : StagingObjectBase
    {
        public HisDepositReasonSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_DEPOSIT_REASON, bool>>> listHisDepositReasonExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DEPOSIT_REASON, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_DEPOSIT_REASON, bool>>> listVHisDepositReasonExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DEPOSIT_REASON, bool>>>();
    }
}
