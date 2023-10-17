using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisDebtGoodsSO : StagingObjectBase
    {
        public HisDebtGoodsSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_DEBT_GOODS, bool>>> listHisDebtGoodsExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DEBT_GOODS, bool>>>();
    }
}
