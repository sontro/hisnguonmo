using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisDeskSO : StagingObjectBase
    {
        public HisDeskSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_DESK, bool>>> listHisDeskExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DESK, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_DESK, bool>>> listVHisDeskExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DESK, bool>>>();
    }
}
