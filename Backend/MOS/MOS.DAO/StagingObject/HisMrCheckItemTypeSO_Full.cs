using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMrCheckItemTypeSO : StagingObjectBase
    {
        public HisMrCheckItemTypeSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MR_CHECK_ITEM_TYPE, bool>>> listHisMrCheckItemTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MR_CHECK_ITEM_TYPE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MR_CHECK_ITEM_TYPE, bool>>> listVHisMrCheckItemTypeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MR_CHECK_ITEM_TYPE, bool>>>();
    }
}
