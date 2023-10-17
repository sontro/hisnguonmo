using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisFinancePeriodSO : StagingObjectBase
    {
        public HisFinancePeriodSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_FINANCE_PERIOD, bool>>> listHisFinancePeriodExpression = new List<System.Linq.Expressions.Expression<Func<HIS_FINANCE_PERIOD, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_FINANCE_PERIOD, bool>>> listVHisFinancePeriodExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_FINANCE_PERIOD, bool>>>();
    }
}
