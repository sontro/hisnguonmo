using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisPeriodDriverDitySO : StagingObjectBase
    {
        public HisPeriodDriverDitySO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_PERIOD_DRIVER_DITY, bool>>> listHisPeriodDriverDityExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PERIOD_DRIVER_DITY, bool>>>();
    }
}
