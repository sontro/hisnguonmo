using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisRefectorySO : StagingObjectBase
    {
        public HisRefectorySO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_REFECTORY, bool>>> listHisRefectoryExpression = new List<System.Linq.Expressions.Expression<Func<HIS_REFECTORY, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_REFECTORY, bool>>> listVHisRefectoryExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_REFECTORY, bool>>>();
    }
}
