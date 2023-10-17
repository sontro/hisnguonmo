using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisBedLogSO : StagingObjectBase
    {
        public HisBedLogSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_BED_LOG, bool>>> listHisBedLogExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BED_LOG, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_BED_LOG, bool>>> listVHisBedLogExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BED_LOG, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_BED_LOG_1, bool>>> listVHisBedLog1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BED_LOG_1, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_BED_LOG_3, bool>>> listVHisBedLog3Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BED_LOG_3, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_BED_LOG_4, bool>>> listVHisBedLog4Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BED_LOG_4, bool>>>();
    }
}
