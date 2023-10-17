using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMrCheckItemSO : StagingObjectBase
    {
        public HisMrCheckItemSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MR_CHECK_ITEM, bool>>> listHisMrCheckItemExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MR_CHECK_ITEM, bool>>>();
    }
}
