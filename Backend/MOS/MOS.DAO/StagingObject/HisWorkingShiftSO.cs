using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisWorkingShiftSO : StagingObjectBase
    {
        public HisWorkingShiftSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_WORKING_SHIFT, bool>>> listHisWorkingShiftExpression = new List<System.Linq.Expressions.Expression<Func<HIS_WORKING_SHIFT, bool>>>();
    }
}
