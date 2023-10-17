using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisPtttGroupBestSO : StagingObjectBase
    {
        public HisPtttGroupBestSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_PTTT_GROUP_BEST, bool>>> listHisPtttGroupBestExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PTTT_GROUP_BEST, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_PTTT_GROUP_BEST, bool>>> listVHisPtttGroupBestExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PTTT_GROUP_BEST, bool>>>();
    }
}
