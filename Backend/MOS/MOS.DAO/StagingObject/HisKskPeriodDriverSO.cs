using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisKskPeriodDriverSO : StagingObjectBase
    {
        public HisKskPeriodDriverSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_KSK_PERIOD_DRIVER, bool>>> listHisKskPeriodDriverExpression = new List<System.Linq.Expressions.Expression<Func<HIS_KSK_PERIOD_DRIVER, bool>>>();
    }
}
