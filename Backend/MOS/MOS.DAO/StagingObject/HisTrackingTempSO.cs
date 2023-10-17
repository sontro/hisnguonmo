using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisTrackingTempSO : StagingObjectBase
    {
        public HisTrackingTempSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_TRACKING_TEMP, bool>>> listHisTrackingTempExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TRACKING_TEMP, bool>>>();
    }
}
