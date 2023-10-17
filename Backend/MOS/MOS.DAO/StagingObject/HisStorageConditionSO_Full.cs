using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisStorageConditionSO : StagingObjectBase
    {
        public HisStorageConditionSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_STORAGE_CONDITION, bool>>> listHisStorageConditionExpression = new List<System.Linq.Expressions.Expression<Func<HIS_STORAGE_CONDITION, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_STORAGE_CONDITION, bool>>> listVHisStorageConditionExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_STORAGE_CONDITION, bool>>>();
    }
}
