using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisNumOrderBlockSO : StagingObjectBase
    {
        public HisNumOrderBlockSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_NUM_ORDER_BLOCK, bool>>> listHisNumOrderBlockExpression = new List<System.Linq.Expressions.Expression<Func<HIS_NUM_ORDER_BLOCK, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_NUM_ORDER_BLOCK, bool>>> listVHisNumOrderBlockExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_NUM_ORDER_BLOCK, bool>>>();
    }
}
