using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisPositionSO : StagingObjectBase
    {
        public HisPositionSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_POSITION, bool>>> listHisPositionExpression = new List<System.Linq.Expressions.Expression<Func<HIS_POSITION, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_POSITION, bool>>> listVHisPositionExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_POSITION, bool>>>();
    }
}
