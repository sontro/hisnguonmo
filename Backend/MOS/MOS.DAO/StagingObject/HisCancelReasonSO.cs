using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisCancelReasonSO : StagingObjectBase
    {
        public HisCancelReasonSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_CANCEL_REASON, bool>>> listHisCancelReasonExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CANCEL_REASON, bool>>>();
    }
}
