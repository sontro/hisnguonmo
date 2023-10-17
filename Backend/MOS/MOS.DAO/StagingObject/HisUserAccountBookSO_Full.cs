using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisUserAccountBookSO : StagingObjectBase
    {
        public HisUserAccountBookSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_USER_ACCOUNT_BOOK, bool>>> listHisUserAccountBookExpression = new List<System.Linq.Expressions.Expression<Func<HIS_USER_ACCOUNT_BOOK, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_USER_ACCOUNT_BOOK, bool>>> listVHisUserAccountBookExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_USER_ACCOUNT_BOOK, bool>>>();
    }
}
