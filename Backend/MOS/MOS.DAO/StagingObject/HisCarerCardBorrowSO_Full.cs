using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisCarerCardBorrowSO : StagingObjectBase
    {
        public HisCarerCardBorrowSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_CARER_CARD_BORROW, bool>>> listHisCarerCardBorrowExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CARER_CARD_BORROW, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_CARER_CARD_BORROW, bool>>> listVHisCarerCardBorrowExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_CARER_CARD_BORROW, bool>>>();
    }
}
