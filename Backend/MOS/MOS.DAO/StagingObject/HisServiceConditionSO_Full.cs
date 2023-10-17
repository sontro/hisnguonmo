using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisServiceConditionSO : StagingObjectBase
    {
        public HisServiceConditionSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_CONDITION, bool>>> listHisServiceConditionExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_CONDITION, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_CONDITION, bool>>> listVHisServiceConditionExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_CONDITION, bool>>>();
    }
}
