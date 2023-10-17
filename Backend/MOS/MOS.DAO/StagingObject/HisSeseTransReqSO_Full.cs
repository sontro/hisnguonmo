using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisSeseTransReqSO : StagingObjectBase
    {
        public HisSeseTransReqSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SESE_TRANS_REQ, bool>>> listHisSeseTransReqExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SESE_TRANS_REQ, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SESE_TRANS_REQ, bool>>> listVHisSeseTransReqExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SESE_TRANS_REQ, bool>>>();
    }
}
