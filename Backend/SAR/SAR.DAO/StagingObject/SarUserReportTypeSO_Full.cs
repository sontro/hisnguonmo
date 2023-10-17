using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SAR.DAO.StagingObject
{
    public class SarUserReportTypeSO : StagingObjectBase
    {
        public SarUserReportTypeSO()
        {
            listSarUserReportTypeExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
            listVSarUserReportTypeExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_USER_REPORT_TYPE, bool>>> listSarUserReportTypeExpression = new List<System.Linq.Expressions.Expression<Func<SAR_USER_REPORT_TYPE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SAR_USER_REPORT_TYPE, bool>>> listVSarUserReportTypeExpression = new List<System.Linq.Expressions.Expression<Func<V_SAR_USER_REPORT_TYPE, bool>>>();
    }
}
