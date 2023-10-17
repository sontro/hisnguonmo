using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisServiceSameSO : StagingObjectBase
    {
        public HisServiceSameSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_SAME, bool>>> listHisServiceSameExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_SAME, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_SAME, bool>>> listVHisServiceSameExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_SAME, bool>>>();
    }
}
