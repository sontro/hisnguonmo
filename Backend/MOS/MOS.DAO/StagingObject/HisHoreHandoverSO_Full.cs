using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisHoreHandoverSO : StagingObjectBase
    {
        public HisHoreHandoverSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_HORE_HANDOVER, bool>>> listHisHoreHandoverExpression = new List<System.Linq.Expressions.Expression<Func<HIS_HORE_HANDOVER, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_HORE_HANDOVER, bool>>> listVHisHoreHandoverExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_HORE_HANDOVER, bool>>>();
    }
}
