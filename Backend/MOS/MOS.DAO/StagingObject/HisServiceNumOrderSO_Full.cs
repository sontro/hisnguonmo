using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisServiceNumOrderSO : StagingObjectBase
    {
        public HisServiceNumOrderSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_NUM_ORDER, bool>>> listHisServiceNumOrderExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_NUM_ORDER, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_NUM_ORDER, bool>>> listVHisServiceNumOrderExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_NUM_ORDER, bool>>>();
    }
}
