using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisTransactionExpSO : StagingObjectBase
    {
        public HisTransactionExpSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_TRANSACTION_EXP, bool>>> listHisTransactionExpExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TRANSACTION_EXP, bool>>>();
    }
}
