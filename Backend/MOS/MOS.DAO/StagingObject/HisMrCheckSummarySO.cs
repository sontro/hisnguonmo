using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMrCheckSummarySO : StagingObjectBase
    {
        public HisMrCheckSummarySO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MR_CHECK_SUMMARY, bool>>> listHisMrCheckSummaryExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MR_CHECK_SUMMARY, bool>>>();
    }
}
