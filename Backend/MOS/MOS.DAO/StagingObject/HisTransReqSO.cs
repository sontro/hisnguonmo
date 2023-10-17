using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisTransReqSO : StagingObjectBase
    {
        public HisTransReqSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_TRANS_REQ, bool>>> listHisTransReqExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TRANS_REQ, bool>>>();
    }
}
