using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisSpeedUnitSO : StagingObjectBase
    {
        public HisSpeedUnitSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SPEED_UNIT, bool>>> listHisSpeedUnitExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SPEED_UNIT, bool>>>();
    }
}
