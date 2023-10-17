using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisRationSumSO : StagingObjectBase
    {
        public HisRationSumSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_RATION_SUM, bool>>> listHisRationSumExpression = new List<System.Linq.Expressions.Expression<Func<HIS_RATION_SUM, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_RATION_SUM, bool>>> listVHisRationSumExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_RATION_SUM, bool>>>();
    }
}
