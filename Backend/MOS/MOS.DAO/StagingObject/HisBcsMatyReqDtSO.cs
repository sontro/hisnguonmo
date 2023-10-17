using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisBcsMatyReqDtSO : StagingObjectBase
    {
        public HisBcsMatyReqDtSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_BCS_MATY_REQ_DT, bool>>> listHisBcsMatyReqDtExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BCS_MATY_REQ_DT, bool>>>();
    }
}
