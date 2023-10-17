using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisServiceChangeReqSO : StagingObjectBase
    {
        public HisServiceChangeReqSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_CHANGE_REQ, bool>>> listHisServiceChangeReqExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_CHANGE_REQ, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_CHANGE_REQ, bool>>> listVHisServiceChangeReqExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_CHANGE_REQ, bool>>>();
    }
}
