using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SAR.DAO.StagingObject
{
    public class SarReportTypeSO : StagingObjectBase
    {
        public SarReportTypeSO()
        {
            listSarReportTypeExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
            listVSarReportTypeExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_REPORT_TYPE, bool>>> listSarReportTypeExpression = new List<System.Linq.Expressions.Expression<Func<SAR_REPORT_TYPE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SAR_REPORT_TYPE, bool>>> listVSarReportTypeExpression = new List<System.Linq.Expressions.Expression<Func<V_SAR_REPORT_TYPE, bool>>>();
    }
}
