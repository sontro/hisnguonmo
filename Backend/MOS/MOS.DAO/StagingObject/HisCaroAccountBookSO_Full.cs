using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisCaroAccountBookSO : StagingObjectBase
    {
        public HisCaroAccountBookSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_CARO_ACCOUNT_BOOK, bool>>> listHisCaroAccountBookExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CARO_ACCOUNT_BOOK, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_CARO_ACCOUNT_BOOK, bool>>> listVHisCaroAccountBookExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_CARO_ACCOUNT_BOOK, bool>>>();
    }
}
