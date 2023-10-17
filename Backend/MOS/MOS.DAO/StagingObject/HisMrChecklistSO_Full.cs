using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMrChecklistSO : StagingObjectBase
    {
        public HisMrChecklistSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MR_CHECKLIST, bool>>> listHisMrChecklistExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MR_CHECKLIST, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MR_CHECKLIST, bool>>> listVHisMrChecklistExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MR_CHECKLIST, bool>>>();
    }
}
