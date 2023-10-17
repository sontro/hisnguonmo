using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisUnlimitReasonSO : StagingObjectBase
    {
        public HisUnlimitReasonSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_UNLIMIT_REASON, bool>>> listHisUnlimitReasonExpression = new List<System.Linq.Expressions.Expression<Func<HIS_UNLIMIT_REASON, bool>>>();
    }
}
